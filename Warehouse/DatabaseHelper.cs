using System;
using System.Data;
using Npgsql;

namespace WarehouseProject
{
    public static class DatabaseHelper
    {
        // Укажите свои реальные параметры подключения
        private static readonly string _connectionString =
            "Host=localhost;Port=5432;Database=warehouse;Username=postgres;Password=1";

        //--------------------------------------------------
        // 1. Пользователи и роли
        //--------------------------------------------------

        /// <summary>
        /// Проверить пользователя по логину/паролю.
        /// Возвращает: 
        ///   (true, "Администратор"/"Менеджер"/"Кладовщик") - если успешен вход,
        ///   (false, "") - если не найдено или пароль неверный.
        /// </summary>
        public static (bool success, string roleName) CheckUserCredentials(string login, string password)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                string sql = @"
                    SELECT r.name
                    FROM users u
                    JOIN roles r ON u.role_id = r.id
                    WHERE u.login = @login AND u.password = @password
                    LIMIT 1
                ";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("login", login);
                    cmd.Parameters.AddWithValue("password", password);

                    object result = cmd.ExecuteScalar(); // null, если не найдёт
                    if (result != null)
                    {
                        return (true, result.ToString());
                    }
                    else
                    {
                        return (false, "");
                    }
                }
            }
        }

        //--------------------------------------------------
        // 2. Товары (Products)
        //--------------------------------------------------

        public static DataTable GetAllProducts()
        {
            DataTable dt = new DataTable();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT id, name, quantity FROM products ORDER BY id";

                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    using (var adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public static void InsertProduct(string name, int quantity)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO products (name, quantity) 
                                 VALUES (@name, @quantity)";

                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("name", name);
                    cmd.Parameters.AddWithValue("quantity", quantity);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateProduct(int id, string name, int quantity)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE products 
                                 SET name=@name, quantity=@quantity 
                                 WHERE id=@id";

                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("name", name);
                    cmd.Parameters.AddWithValue("quantity", quantity);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteProduct(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"DELETE FROM products WHERE id=@id";

                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //--------------------------------------------------
        // 3. Поставки (Supplies)
        //--------------------------------------------------

        public static DataTable GetAllSupplies()
        {
            DataTable dt = new DataTable();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT s.id, s.supply_date, p.name AS product_name,
                           s.quantity, s.supplier
                    FROM supplies s
                    JOIN products p ON s.product_id = p.id
                    ORDER BY s.id
                ";

                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    using (var adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public static void InsertSupply(int productId, int quantity, string supplier)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                // 1) Вставляем запись о поставке
                string query = @"INSERT INTO supplies (product_id, quantity, supplier) 
                                 VALUES (@pid, @qty, @sup)";
                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("pid", productId);
                    cmd.Parameters.AddWithValue("qty", quantity);
                    cmd.Parameters.AddWithValue("sup", supplier ?? "");
                    cmd.ExecuteNonQuery();
                }

                // 2) Обновляем количество товара
                string updateProduct = @"UPDATE products 
                                        SET quantity = quantity + @qty 
                                        WHERE id = @pid";
                using (var cmd2 = new NpgsqlCommand(updateProduct, connection))
                {
                    cmd2.Parameters.AddWithValue("qty", quantity);
                    cmd2.Parameters.AddWithValue("pid", productId);
                    cmd2.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteSupply(int supplyId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"DELETE FROM supplies WHERE id=@id";

                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("id", supplyId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //--------------------------------------------------
        // 4. Заказы (Orders)
        //--------------------------------------------------

        public static DataTable GetAllOrders()
        {
            DataTable dt = new DataTable();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT o.id, o.order_date, p.name AS product_name,
                           o.quantity, o.status
                    FROM orders o
                    JOIN products p ON o.product_id = p.id
                    ORDER BY o.id
                ";

                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    using (var adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public static void InsertOrder(int productId, int quantity, string status)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                // 1) Добавляем заказ
                string query = @"INSERT INTO orders (product_id, quantity, status) 
                                 VALUES (@pid, @qty, @status)";
                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("pid", productId);
                    cmd.Parameters.AddWithValue("qty", quantity);
                    cmd.Parameters.AddWithValue("status", status);
                    cmd.ExecuteNonQuery();
                }

                // 2) Уменьшаем остаток товара (если хватает на складе)
                string updateProduct = @"UPDATE products 
                                        SET quantity = quantity - @qty 
                                        WHERE id = @pid AND quantity >= @qty";
                using (var cmd2 = new NpgsqlCommand(updateProduct, connection))
                {
                    cmd2.Parameters.AddWithValue("qty", quantity);
                    cmd2.Parameters.AddWithValue("pid", productId);
                    cmd2.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateOrder(int orderId, string status)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"UPDATE orders 
                                 SET status=@status 
                                 WHERE id=@id";
                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("id", orderId);
                    cmd.Parameters.AddWithValue("status", status);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteOrder(int orderId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"DELETE FROM orders WHERE id=@id";

                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("id", orderId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //--------------------------------------------------
        // 5. Отчёты
        //--------------------------------------------------

        /// <summary>
        /// Отчет по остаткам товаров (возвращаем DataTable).
        /// </summary>
        public static DataTable GetStockReport()
        {
            DataTable dt = new DataTable();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"SELECT id, name, quantity 
                                 FROM products 
                                 ORDER BY id";
                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    using (var adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// Отчет по популярным товарам (пример).
        /// Считает суммарные заказы по каждому товару.
        /// </summary>
        public static DataTable GetPopularProductsReport()
        {
            DataTable dt = new DataTable();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT p.id, p.name, 
                           SUM(o.quantity) AS total_ordered
                    FROM products p
                    LEFT JOIN orders o ON p.id = o.product_id
                    GROUP BY p.id, p.name
                    ORDER BY total_ordered DESC NULLS LAST
                ";
                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    using (var adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }
    }
}
