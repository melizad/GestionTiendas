using System;

namespace GestionTiendas
{
    class Program
    {
        // Credencial
        static string[] credencialesPorDefecto = { "00000", "12345" };

        // Usuarios
        static string[,] usuarios = new string[15, 5];
        static int totalUsuarios = 0;

        // Artículos
        static string[,] articulos = new string[15, 4];
        static int totalArticulos = 0;
        static int idAutoincrementalArticulo = 1;

        static void Main(string[] args)
        {
            Console.Title = "Sistema de Gestión de Tiendas";

            // Bucle infinito: al cerrar sesión, vuelve a la pantalla de autenticación
            while (true)
            {
                Autenticacion();
                MenuPrincipal();
            }
        }

        // ==========================================
        // MÓDULO: AUTENTICACIÓN
        // ==========================================
        static void Autenticacion()
        {
            bool autenticado = false;
            while (!autenticado)
            {
                Console.Clear();
                Console.WriteLine("===============================");
                Console.WriteLine(" SISTEMA DE GESTIÓN DE TIENDAS ");
                Console.WriteLine("===============================");
                Console.WriteLine("\n--- Iniciar Sesión ---");

                Console.Write("Usuario: ");
                string user = Console.ReadLine();

                Console.Write("Contraseña: ");
                string pass = Console.ReadLine();

                if (user == credencialesPorDefecto[0] && pass == credencialesPorDefecto[1])
                {
                    autenticado = true;
                    Console.WriteLine("\n¡Autenticación exitosa!");
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    Console.WriteLine("\nDatos incorrectos. Presione ENTER para intentar de nuevo...");
                    Console.ReadLine();
                }
            }
        }

        // ==========================================
        // MÓDULO: MENÚ PRINCIPAL
        // ==========================================
        static void MenuPrincipal()
        {
            bool enSistema = true;
            while (enSistema)
            {
                Console.Clear();
                Console.WriteLine("===============================");
                Console.WriteLine("        MENÚ PRINCIPAL         ");
                Console.WriteLine("===============================");
                Console.WriteLine("1. Gestión de usuarios");
                Console.WriteLine("2. Gestión de artículos");
                Console.WriteLine("3. Gestión de ventas");
                Console.WriteLine("4. Salir del programa");
                Console.Write("\nSeleccione una opción: ");

                string op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        MenuUsuarios();
                        break;
                    case "2":
                        MenuArticulos();
                        break;
                    case "3":
                        MenuVentas();
                        break;
                    case "4":
                        enSistema = false; // Rompe el bucle y regresa a Autenticacion
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Presione ENTER para continuar.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        // ==========================================
        // MÓDULO: GESTIÓN DE USUARIOS
        // ==========================================
        static void MenuUsuarios()
        {
            bool salirMenu = false;
            while (!salirMenu)
            {
                Console.Clear();
                Console.WriteLine(
                    "\n---------------------" +
                    "\n-Gestión de usuarios-" +
                    "\n---------------------");
                Console.WriteLine("1. Ver lista de usuarios");
                Console.WriteLine("2. Nuevo usuario");
                Console.WriteLine("3. Editar información de usuario");
                Console.WriteLine("4. Salir de Gestión de usuarios");
                Console.Write("\nSeleccione: ");

                string op = Console.ReadLine();

                switch (op)
                {
                    case "1": VerListaUsuarios(); break;
                    case "2": NuevoUsuario(); break;
                    case "3": EditarUsuario(); break;
                    case "4": salirMenu = true; break;
                    default: Console.WriteLine("Opción no válida."); Console.ReadLine(); break;
                }
            }
        }

        static void VerListaUsuarios()
        {
            Console.Clear();
            Console.WriteLine("\nLista de usuarios:");
            if (totalUsuarios == 0)
            {
                Console.WriteLine("No hay usuarios registrados.");
            }
            else
            {
                for (int i = 0; i < totalUsuarios; i++)
                {
                    Console.WriteLine($"ID: {usuarios[i, 0]} | Nombre: {usuarios[i, 1]} {usuarios[i, 2]} | Tel: {usuarios[i, 3]} | Dir: {usuarios[i, 4]}");
                }
            }
            Console.WriteLine("\nPresione ENTER para continuar...");
            Console.ReadLine();
        }

        static void NuevoUsuario()
        {
            Console.Clear();
            Console.WriteLine("\nCrear Nuevos Usuarios:");
            if (totalUsuarios >= 15)
            {
                Console.WriteLine("No se permiten crear usuarios nuevos. Límite de 15 alcanzado.");
            }
            else
            {
                Console.Write("Número de Identificación: ");
                usuarios[totalUsuarios, 0] = Console.ReadLine();
                Console.Write("Nombres: ");
                usuarios[totalUsuarios, 1] = Console.ReadLine();
                Console.Write("Apellidos: ");
                usuarios[totalUsuarios, 2] = Console.ReadLine();
                Console.Write("Teléfono: ");
                usuarios[totalUsuarios, 3] = Console.ReadLine();
                Console.Write("Dirección: ");
                usuarios[totalUsuarios, 4] = Console.ReadLine();

                totalUsuarios++;
                Console.WriteLine("\n¡Usuario creado con éxito!");
            }
            Console.WriteLine("Presione ENTER para continuar...");
            Console.ReadLine();
        }

        static void EditarUsuario()
        {
            Console.Clear();
            Console.WriteLine("\nEditar usuario");
            Console.Write("Ingrese el Número de Identificación a buscar: ");
            string idBuscar = Console.ReadLine();

            int indiceEncontrado = -1;
            for (int i = 0; i < totalUsuarios; i++)
            {
                if (usuarios[i, 0] == idBuscar)
                {
                    indiceEncontrado = i;
                    break;
                }
            }

            if (indiceEncontrado != -1)
            {
                Console.WriteLine($"\nUsuario encontrado: {usuarios[indiceEncontrado, 1]} {usuarios[indiceEncontrado, 2]}");
                Console.WriteLine("¿Qué dato desea editar?");
                Console.WriteLine(
                    "1. Nombres" +
                    "\n2. Apellidos" +
                    "\n3. Teléfono" +
                    "\n4. Dirección");
                Console.Write("Seleccione: ");
                string op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        Console.Write("Nuevo Nombre: ");
                        usuarios[indiceEncontrado, 1] = Console.ReadLine();
                        break;
                    case "2":
                        Console.Write("Nuevos Apellidos: ");
                        usuarios[indiceEncontrado, 2] = Console.ReadLine();
                        break;
                    case "3":
                        Console.Write("Nuevo Teléfono: ");
                        usuarios[indiceEncontrado, 3] = Console.ReadLine();
                        break;
                    case "4":
                        Console.Write("Nueva Dirección: ");
                        usuarios[indiceEncontrado, 4] = Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
                Console.WriteLine("Proceso finalizado.");
            }
            else
            {
                Console.WriteLine("Usuario no encontrado.");
            }
            Console.WriteLine("Presione ENTER para continuar...");
            Console.ReadLine();
        }

        // ==========================================
        // MÓDULO: GESTIÓN DE ARTÍCULOS
        // ==========================================
        static void MenuArticulos()
        {
            bool salirMenu = false;
            while (!salirMenu)
            {
                Console.Clear();
                Console.WriteLine(
                    "\n----------------------" +
                    "\n-Gestión de Artículos-" +
                    "\n----------------------");
                Console.WriteLine("1. Ver lista de artículos");
                Console.WriteLine("2. Nuevo artículo");
                Console.WriteLine("3. Editar información del artículo");
                Console.WriteLine("4. Salir de Gestión de Artículos");
                Console.Write("\nSeleccione: ");

                string op = Console.ReadLine();

                switch (op)
                {
                    case "1": VerListaArticulos(); break;
                    case "2": NuevoArticulo(); break;
                    case "3": EditarArticulo(); break;
                    case "4": salirMenu = true; break;
                    default: Console.WriteLine("Opción inválida."); Console.ReadLine(); break;
                }
            }
        }

        static void VerListaArticulos()
        {
            Console.Clear();
            Console.WriteLine("\nLista de Artículos:");
            if (totalArticulos == 0)
            {
                Console.WriteLine("No hay artículos registrados.");
            }
            else
            {
                for (int i = 0; i < totalArticulos; i++)
                {
                    Console.WriteLine($"ID: {articulos[i, 0]} | Nombre: {articulos[i, 1]} | Valor Und: ${articulos[i, 2]} | Stock: {articulos[i, 3]}");
                }
            }
            Console.WriteLine("\nPresione ENTER para continuar...");
            Console.ReadLine();
        }

        static void NuevoArticulo()
        {
            Console.Clear();
            Console.WriteLine("\nCrear Nuevos Artículos:");
            if (totalArticulos >= 15)
            {
                Console.WriteLine("No se permiten crear artículos nuevos. Límite de 15 alcanzado.");
            }
            else
            {
                // Generación automática ID
                articulos[totalArticulos, 0] = idAutoincrementalArticulo.ToString();
                Console.WriteLine($"ID del Artículo (Generado automáticamente): {idAutoincrementalArticulo}");

                Console.Write("Nombre del artículo: ");
                articulos[totalArticulos, 1] = Console.ReadLine();

                Console.Write("Valor Unitario: ");
                articulos[totalArticulos, 2] = Console.ReadLine();

                Console.Write("Cantidad en Stock: ");
                articulos[totalArticulos, 3] = Console.ReadLine();

                totalArticulos++;
                idAutoincrementalArticulo++;
                Console.WriteLine("\n¡Artículo creado con éxito!");
            }
            Console.WriteLine("Presione ENTER para continuar...");
            Console.ReadLine();
        }

        static void EditarArticulo()
        {
            Console.Clear();
            Console.WriteLine("\nEditar artículo");
            Console.Write("Ingrese el ID del artículo a buscar: ");
            string idBuscar = Console.ReadLine();

            int indiceEncontrado = -1;
            for (int i = 0; i < totalArticulos; i++)
            {
                if (articulos[i, 0] == idBuscar)
                {
                    indiceEncontrado = i;
                    break;
                }
            }

            if (indiceEncontrado != -1)
            {
                Console.WriteLine($"\nArtículo encontrado: {articulos[indiceEncontrado, 1]}");
                Console.WriteLine("¿Qué dato desea editar?");
                Console.WriteLine("1. Nombre\n2. Valor Unitario\n3. Cantidad en Stock");
                Console.Write("Seleccione: ");
                string op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        Console.Write("Nuevo Nombre: ");
                        articulos[indiceEncontrado, 1] = Console.ReadLine();
                        break;
                    case "2":
                        Console.Write("Nuevo Valor Unitario: ");
                        articulos[indiceEncontrado, 2] = Console.ReadLine();
                        break;
                    case "3":
                        Console.Write("Nueva Cantidad en Stock: ");
                        articulos[indiceEncontrado, 3] = Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
                Console.WriteLine("Proceso finalizado.");
            }
            else
            {
                Console.WriteLine("Artículo no encontrado.");
            }
            Console.WriteLine("Presione ENTER para continuar...");
            Console.ReadLine();
        }

        // ==========================================
        // MÓDULO: GESTIÓN DE VENTAS
        // ==========================================
        static void MenuVentas()
        {
            bool salirMenu = false;
            while (!salirMenu)
            {
                Console.Clear();
                Console.WriteLine(
                    "\n-------------------" +
                    "\n-Gestión de Ventas-" +
                    "\n-------------------");
                Console.WriteLine("1. Generar factura");
                Console.WriteLine("2. Salir de Gestión Ventas");
                Console.Write("\nSeleccione: ");

                string op = Console.ReadLine();

                if (op == "1") GenerarFactura();
                else if (op == "2") salirMenu = true;
                else { Console.WriteLine("Opción inválida."); Console.ReadLine(); }
            }
        }

        static void GenerarFactura()
        {
            Console.Clear();
            Console.WriteLine("--- GENERAR FACTURA ---");
            Console.WriteLine(
                    "\n-----------------" +
                    "\n-Generar factura-" +
                    "\n-----------------");

            // 1. Buscar y elegir comprador
            Console.Write("Ingrese el Número de Identificación del cliente: ");
            string idComprador = Console.ReadLine();

            int indiceUsuario = -1;
            for (int i = 0; i < totalUsuarios; i++)
            {
                if (usuarios[i, 0] == idComprador)
                {
                    indiceUsuario = i;
                    break;
                }
            }

            if (indiceUsuario == -1)
            {
                Console.WriteLine("Usuario no encontrado. Debe registrar al cliente antes de vender.");
                Console.ReadLine();
                return; // Corta la venta y regresa al menú de ventas
            }

            Console.WriteLine($"Cliente seleccionado: {usuarios[indiceUsuario, 1]} {usuarios[indiceUsuario, 2]}");

            // 2. Elegir productos a comprar
            // Matriz para guardar el detalle de factura: ID, Nombre, VUnitario, Cantidad, Subtotal
            string[,] detalleFactura = new string[10, 5];
            int contadorProductosFactura = 0;
            double granTotal = 0;
            bool agregandoProductos = true;

            while (agregandoProductos && contadorProductosFactura < 10)
            {
                Console.WriteLine("\n--- Artículos Disponibles ---");
                for (int i = 0; i < totalArticulos; i++)
                {
                    Console.WriteLine($"ID: {articulos[i, 0]} | {articulos[i, 1]} | ${articulos[i, 2]} | Disp: {articulos[i, 3]}");
                }

                Console.Write("\nIngrese el ID del producto que desea comprar: ");
                string idProd = Console.ReadLine();

                int indiceArt = -1;
                for (int i = 0; i < totalArticulos; i++)
                {
                    if (articulos[i, 0] == idProd)
                    {
                        indiceArt = i;
                        break;
                    }
                }

                if (indiceArt != -1)
                {
                    Console.Write($"¿Qué cantidad de {articulos[indiceArt, 1]} desea?: ");
                    if (int.TryParse(Console.ReadLine(), out int cantidadSol))
                    {
                        int stockDisponible = int.Parse(articulos[indiceArt, 3]);

                        if (cantidadSol > 0 && cantidadSol <= stockDisponible)
                        {
                            // Calcular y descontar stock
                            double vUnitario = double.Parse(articulos[indiceArt, 2]);
                            double subtotal = vUnitario * cantidadSol;

                            articulos[indiceArt, 3] = (stockDisponible - cantidadSol).ToString(); // Restar stock

                            // Guardar en detalle de factura
                            detalleFactura[contadorProductosFactura, 0] = articulos[indiceArt, 0];
                            detalleFactura[contadorProductosFactura, 1] = articulos[indiceArt, 1];
                            detalleFactura[contadorProductosFactura, 2] = articulos[indiceArt, 2];
                            detalleFactura[contadorProductosFactura, 3] = cantidadSol.ToString();
                            detalleFactura[contadorProductosFactura, 4] = subtotal.ToString();

                            granTotal += subtotal;
                            contadorProductosFactura++;

                            Console.WriteLine("¡Producto agregado a la factura!");
                        }
                        else
                        {
                            Console.WriteLine("Cantidad inválida o excede el stock disponible.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("ID de producto no encontrado.");
                }

                if (contadorProductosFactura < 10)
                {
                    Console.Write("\n¿Desea agregar otro producto? (S/N): ");
                    if (Console.ReadLine().ToUpper() != "S") agregandoProductos = false;
                }
                else
                {
                    Console.WriteLine("\nLímite máximo de 10 productos diferentes alcanzado.");
                }
            }

            // 3. Imprimir Factura en Pantalla
            if (contadorProductosFactura > 0)
            {
                Console.Clear();
                Console.WriteLine("======================================================");
                Console.WriteLine("                   FACTURA DE VENTA                   ");
                Console.WriteLine("======================================================");
                Console.WriteLine($"Comprador: {usuarios[indiceUsuario, 1]} {usuarios[indiceUsuario, 2]}");
                Console.WriteLine($"Identificación: {usuarios[indiceUsuario, 0]}");
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("{0,-5} {1,-15} {2,-10} {3,-8} {4,-10}", "ID", "Producto", "V.Und", "Cant", "Subtotal");
                Console.WriteLine("------------------------------------------------------");

                for (int i = 0; i < contadorProductosFactura; i++)
                {
                    Console.WriteLine("{0,-5} {1,-15} ${2,-9} {3,-8} ${4,-9}",
                        detalleFactura[i, 0],
                        detalleFactura[i, 1],
                        detalleFactura[i, 2],
                        detalleFactura[i, 3],
                        detalleFactura[i, 4]);
                }

                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine($"TOTAL A PAGAR: ${granTotal}");
                Console.WriteLine("======================================================");
            }
            else
            {
                Console.WriteLine("\nLa venta fue cancelada porque no se agregaron productos.");
            }

            Console.WriteLine("\nPresione ENTER para salir de la factura...");
            Console.ReadLine();
        }
    }
}