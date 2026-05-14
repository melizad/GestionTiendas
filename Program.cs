using System;

namespace GestionTiendas
{
    class Program
    {
        static string[] credencialesPorDefecto = { "00000", "12345" };

        static string[,] matrizUsuarios = new string[2, 5];
        static string[,] matrizNuevosUsuarios = new string[2, 5];
        static int totalNuevosUsuarios = 0;

        static string[,] matrizArticulos = new string[2, 5];
        static string[,] matrizNuevosArticulos = new string[2, 5];
        static int totalNuevosArticulos = 0;

        static string[,] matrizRegistroVenta = new string[2, 5];

        static void Main(string[] args)
        {
            CargarUsuariosBase();
            CargarArticulosBase();
            Console.WriteLine("\n===============================");
            Console.WriteLine("=SISTEMA DE GESTIÓN DE TIENDAS=");
            Console.WriteLine("===============================");
            Autenticacion();
        }

        static void CargarUsuariosBase()
        {
            matrizUsuarios[0, 0] = "15386596"; matrizUsuarios[1, 0] = "Ana";
            matrizUsuarios[0, 1] = "39192774"; matrizUsuarios[1, 1] = "Brett";
            matrizUsuarios[0, 2] = "1039311755"; matrizUsuarios[1, 2] = "Carolina";
            matrizUsuarios[0, 3] = "3809765"; matrizUsuarios[1, 3] = "Diego";
            matrizUsuarios[0, 4] = "21386890"; matrizUsuarios[1, 4] = "Estefannia";
        }

        static void CargarArticulosBase()
        {
            matrizArticulos[0, 0] = "Huevos"; matrizArticulos[1, 0] = "500";
            matrizArticulos[0, 1] = "Leche"; matrizArticulos[1, 1] = "2500";
            matrizArticulos[0, 2] = "Harina"; matrizArticulos[1, 2] = "3000";
            matrizArticulos[0, 3] = "Panela"; matrizArticulos[1, 3] = "5000";
            matrizArticulos[0, 4] = "Pan"; matrizArticulos[1, 4] = "4000";
        }

        static void Autenticacion()
        {
            while (true)
            {
                Console.WriteLine("\n------------------\n-Inicio de sesión-\n------------------");
                Console.Write("Usuario: ");
                string u = Console.ReadLine();
                Console.Write("Contraseña: ");
                string c = Console.ReadLine();

                if (u == credencialesPorDefecto[0] && c == credencialesPorDefecto[1])
                {
                    Console.WriteLine("\n¡Bienvenido!");
                    MenuPrincipal();
                }
                else
                {
                    Console.WriteLine("\nError de autenticación. Intente de nuevo.");
                }
            }
        }

        static void MenuPrincipal()
        {
            bool sesionActiva = true;
            while (sesionActiva)
            {
                Console.WriteLine(
                    "\n----------------" +
                    "\n-Menú Principal-" +
                    "\n----------------");
                Console.WriteLine("1. Gestión de usuarios" +
                    "\n2. Gestión de artículos" +
                    "\n3. Gestión de ventas" +
                    "\n4. Salir del programa");
                Console.Write("Seleccione: ");
                string op = Console.ReadLine();

                if (op == "1")
                {
                    Console.WriteLine("\nBienvenido al módulo Gestión de usuarios.");
                    GestionarUsuarios();
                }

                else if (op == "2")
                {
                    Console.WriteLine("\nBienvenido al módulo Gestión de artículos.");
                    GestionarArticulos();
                }
                else if (op == "3") 
                {
                    Console.WriteLine("\nBienvenido al módulo Gestión de ventas.");
                    GestionarVentas();
                } 
                else if (op == "4")
                {
                    Console.WriteLine("\nCerrando sesión...");
                    sesionActiva = false;
                }
                else Console.WriteLine("Ingrese una opción del menú válida.");
            }
        }

        // --- MÉTODOS DE GESTIÓN DE USUARIOS ---
        static void GestionarUsuarios()
        {
            bool salirGestion = false;
            while (!salirGestion)
            {
                Console.WriteLine(
                    "\n---------------------" +
                    "\n-Gestión de usuarios-" +
                    "\n---------------------");
                Console.WriteLine(
                    "1. Ver lista de usuarios" +
                    "\n2. Nuevo usuario" +
                    "\n3. Editar inforamción de usuario" +
                    "\n4. Salir de Gestión de usuarios ");
                Console.Write("Seleccione: ");
                string op = Console.ReadLine();
                if (op == "1") VerListaUsuarios();
                else if (op == "2") CrearNuevaMatrizUsuario();
                else if (op == "3") BuscarUsuario();
                else if (op == "4") salirGestion = true;
                else Console.WriteLine("\nIngrese una opción del menú válida");

            }
        }

        static void VerListaUsuarios()
        {
            Console.WriteLine("\nLista de usuarios:");
            for (int i = 0; i < 5; i++)
                Console.WriteLine($"{i + 1}. {(string.IsNullOrEmpty(matrizUsuarios[0, i]) ? "Vacío" : matrizUsuarios[0, i])}");

            bool seleccionValida = false;
            while (!seleccionValida)
            {
                Console.Write("\nSeleccione un número para ver detalle (1-5): ");
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out int sel) && sel >= 1 && sel <= 5)
                {
                    int i = sel - 1; 

                    Console.WriteLine("-------------------");
                    Console.WriteLine($"Cédula: {matrizUsuarios[0, i]}");
                    Console.WriteLine($"Nombre: {matrizUsuarios[1, i]}");
                    Console.WriteLine("-------------------");
                    
                    seleccionValida = true; 
                }
                else
                {
                    Console.WriteLine("Ingrese una opción del menú válida");
                }
            }
        }

        static void CrearNuevaMatrizUsuario()
        {
            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine("\nCrear Nuevos Usuarios:");
                Console.WriteLine("1. Crear nuevo usuario");
                Console.WriteLine("2. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                if (opcion == "1")
                {
                    if (totalNuevosUsuarios < 5)
                    {
                        Console.WriteLine($"\nRegistrando usuario #{totalNuevosUsuarios + 1}");

                        Console.Write("Ingrese número de cédula: ");
                        matrizNuevosUsuarios[0, totalNuevosUsuarios] = Console.ReadLine();

                        Console.Write("Ingrese nombre completo: ");
                        matrizNuevosUsuarios[1, totalNuevosUsuarios] = Console.ReadLine();

                        totalNuevosUsuarios++;
                        Console.WriteLine("¡Usuario almacenado con éxito en la nueva matriz!");
                    }
                    else
                    {
                        Console.WriteLine("\nLa matriz está llena. Ya se han creado los 5 usuarios permitidos.");
                    }
                }
                else if (opcion == "2")
                {
                    continuar = false;
                }
                else
                {
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                }
            }
        }

        static void BuscarUsuario()
        {
            Console.Write("\nCédula a buscar: ");
            string buscar = Console.ReadLine();
            bool encontrado = false;
            for (int i = 0; i < 5; i++)
            {
                if (matrizUsuarios[0, i] == buscar && !string.IsNullOrEmpty(buscar))
                {
                    Console.WriteLine($"Usuario encontrado");
                    Console.WriteLine("-------------------");
                    Console.WriteLine($"Cédula: {matrizUsuarios[0, i]}");
                    Console.WriteLine($"Nombre: {matrizUsuarios[1, i]}");
                    Console.WriteLine("-------------------");
                    encontrado = true; break;
                }
            }
            if (!encontrado) Console.WriteLine("Usuario no encontrado.");
        }

        // --- MÉTODOS DE GESTIÓN DE ARTÍCULOS ---
        static void GestionarArticulos()
        {
            bool salir = false;
            while (!salir)
            {
                Console.WriteLine(
                    "\n----------------------" +
                    "\n-Gestión de Artículos-" +
                    "\n----------------------");
                Console.WriteLine(
                    "1. Ver lista de artículos" +
                    "\n2. Nuevo artículo" +
                    "\n3. Editar información del artículo" +
                    "\n4. Salir de Gestión de Artículos");
                Console.Write("Seleccione: ");
                string op = Console.ReadLine();
                if (op == "1") VerListaArticulos();
                else if (op == "2") CrearNuevaMatrizArticulo();
                else if (op == "3") BuscarArticulo();
                else if (op == "4") salir = true;
                else Console.WriteLine("\nIngrese una opción del menú válida");
            }
        }

        static void VerListaArticulos()
        {
            Console.WriteLine("\nLista de Artículos:");
            for (int i = 0; i < 5; i++)
            {
                string articuloMostrar = string.IsNullOrEmpty(matrizArticulos[0, i]) ? "---" : matrizArticulos[0, i];
                Console.WriteLine($"{i + 1}. {articuloMostrar}");
            }

            bool seleccionValida = false;
            while (!seleccionValida)
            {
                Console.Write("\nSeleccione un número para ver detalle (1-5): ");
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out int sel) && sel >= 1 && sel <= 5)
                {
                    int i = sel - 1;

                    Console.WriteLine("---------------------");
                    Console.WriteLine($"Artículo: {matrizArticulos[0, i]}");
                    Console.WriteLine($"Valor Unitario: {matrizArticulos[1, i]}");
                    Console.WriteLine("---------------------");

                    seleccionValida = true;
                }
                else
                {
                    Console.WriteLine("Ingrese una opción del menú válida");
                }
            }
        }

        static void CrearNuevaMatrizArticulo()
        {
            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine("\nCrear Nuevos Artículos:");
                Console.WriteLine("1. Crear nuevo artículo");
                Console.WriteLine("2. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                if (opcion == "1")
                {
                    if (totalNuevosArticulos < 5)
                    {
                        Console.WriteLine($"\nRegistrando artículo #{totalNuevosArticulos + 1}");

                        Console.Write("Nombre del artículo: ");
                        matrizNuevosArticulos[0, totalNuevosArticulos] = Console.ReadLine();

                        Console.Write("Precio unitario: ");
                        matrizNuevosArticulos[1, totalNuevosArticulos] = Console.ReadLine();

                        totalNuevosArticulos++;
                        Console.WriteLine("¡Artículo almacenado con éxito en la nueva matriz!");
                    }
                    else
                    {
                        Console.WriteLine("\nLa matriz está llena. Ya se han creado los 5 artículos permitidos.");
                    }
                }
                else if (opcion == "2")
                {
                    continuar = false;
                }
                else
                {
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                }
            }
        }

        static void BuscarArticulo()
        {
            Console.Write("\nNombre de artículo a buscar: ");
            string buscar = Console.ReadLine();
            bool encontrado = false;

            for (int i = 0; i < 5; i++)
            {
                // Verificamos que la posición no sea nula antes de comparar
                if (!string.IsNullOrEmpty(matrizArticulos[0, i]) &&
                    matrizArticulos[0, i].ToLower() == buscar.ToLower())
                {
                    Console.WriteLine("\nArtículo encontrado");
                    Console.WriteLine("-------------------");
                    Console.WriteLine($"Nombre: {matrizArticulos[0, i]}");
                    Console.WriteLine($"Precio: ${matrizArticulos[1, i]}");
                    Console.WriteLine("-------------------");

                    encontrado = true;
                    break;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("Artículo no encontrado.");
            }
        }

        // --- MÉTODOS DE GESTIÓN DE VENTAS ---
        static void GestionarVentas()
        {
            bool salirGestion = false;
            while (!salirGestion)
            {
                Console.WriteLine(
                    "\n-------------------" +
                    "\n-Gestión de Ventas-" +
                    "\n-------------------");
                Console.WriteLine(
                    "1. Registrar venta" +
                    "\n2. Salir de Gestión de Venta");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                if (opcion == "1")
                {
                    RegistrarNuevaVenta();
                }
                else if (opcion == "2")
                {
                    salirGestion = true;
                }
                else
                {
                    Console.WriteLine("\nIngrese una opción del menú válida");
                }
            }
        }

        static void RegistrarNuevaVenta()
        {
            string[,] matrizRegistroVenta = new string[2, 5];
            int[] cantidadesVenta = new int[5];
            int contadorArticulos = 0;
            double totalVenta = 0;

            Console.Write("\nIngrese el nombre del comprador: ");
            string nombreComprador = Console.ReadLine();
            Console.Write("Ingrese su número de cédula: ");
            string cedulaComprador = Console.ReadLine();

            bool deseaAgregarMas = true;

            while (deseaAgregarMas)
            {
                if (contadorArticulos >= 5)
                {
                    Console.WriteLine("\n¡Atención! No se pueden registrar más artículos para la venta (Límite: 5).");
                    deseaAgregarMas = false;
                    break;
                }

                int opcionArticulo = 0;
                bool articuloValido = false;

                while (!articuloValido)
                {
                    Console.WriteLine("\nLista de Artículos:");
                    for (int i = 0; i < 5; i++)
                    {
                        string articuloMostrar = string.IsNullOrEmpty(matrizArticulos[0, i]) ? "---" : matrizArticulos[0, i];
                        Console.WriteLine($"{i + 1}. {articuloMostrar}");
                    }
                    Console.Write("\nSeleccione el artículo a comprar (1-5): ");
                    string entrada = Console.ReadLine();

                    if (int.TryParse(entrada, out opcionArticulo) && opcionArticulo >= 1 && opcionArticulo <= 5)
                    {
                        if (string.IsNullOrEmpty(matrizArticulos[0, opcionArticulo - 1]))
                        {
                            Console.WriteLine("Error: El espacio seleccionado está vacío.");
                        }
                        else
                        {
                            articuloValido = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nIngrese una opción del menú válida");
                    }
                }

                int idxStock = opcionArticulo - 1;

                Console.Write($"¿Qué cantidad desea de '{matrizArticulos[0, idxStock]}'? ");
                if (int.TryParse(Console.ReadLine(), out int cant) && cant > 0)
                {
                    double precioUnitario = double.Parse(matrizArticulos[1, idxStock]);
                    double subtotal = cant * precioUnitario;

                    matrizRegistroVenta[0, contadorArticulos] = matrizArticulos[0, idxStock];
                    matrizRegistroVenta[1, contadorArticulos] = subtotal.ToString();
                    cantidadesVenta[contadorArticulos] = cant;

                    totalVenta += subtotal;
                    contadorArticulos++;

                    Console.WriteLine("Artículo agregado correctamente.");
                }
                else
                {
                    Console.WriteLine("Cantidad no válida. El artículo no se agregó.");
                }

                if (contadorArticulos < 5)
                {
                    Console.Write("\n¿Desea agregar otro artículo a la venta? (S/N): ");
                    string respuesta = Console.ReadLine().ToUpper();
                    if (respuesta != "S")
                    {
                        deseaAgregarMas = false;
                    }
                }
                else
                {
                    Console.WriteLine("\nSe ha alcanzado el límite de 5 artículos.");
                    deseaAgregarMas = false;
                }
            }

            Console.WriteLine("\n==========================================");
            Console.WriteLine("            RECIBO DE VENTA               ");
            Console.WriteLine("==========================================");
            Console.WriteLine($"Cliente: {nombreComprador} | Cédula: {cedulaComprador}");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("{0,-15} {1,-10} {2,-10}", "Artículo", "Cant.", "Subtotal");

            for (int i = 0; i < contadorArticulos; i++)
            {
                Console.WriteLine("{0,-15} {1,-10} ${2,-10}",
                    matrizRegistroVenta[0, i],
                    cantidadesVenta[i],
                    matrizRegistroVenta[1, i]);
            }

            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Total Venta: ${totalVenta}");
            Console.WriteLine("==========================================");

        }
    }
}