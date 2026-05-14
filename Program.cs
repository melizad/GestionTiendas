using System;

namespace GestionTiendas
{
    class Program
    {
        // ── Usuarios ──────────────────────────────────────────────
        const int MAX_USUARIOS = 6;
        static string[] usuarioNombres = new string[MAX_USUARIOS];
        static string[] usuarioCedulas = new string[MAX_USUARIOS];
        static string[] usuarioContraseñas = new string[MAX_USUARIOS];
        static int[] usuarioArticulos = new int[MAX_USUARIOS];   // artículos creados por usuario
        static int totalUsuarios = 0;

        // ── Artículos ─────────────────────────────────────────────
        const int MAX_ARTICULOS = 5;
        static string[] articuloNombres = new string[MAX_ARTICULOS];
        static double[] articuloValores = new double[MAX_ARTICULOS];
        static int[] articuloCantidades = new int[MAX_ARTICULOS];
        static int totalArticulos = 0;

        // ── Sesión activa ─────────────────────────────────────────
        static int usuarioLogueadoIdx = -1;

        // ═════════════════════════════════════════════════════════
        static void Main(string[] args)
        {
            // Usuario administrador por defecto (índice 0)
            usuarioNombres[0] = "Administrador";
            usuarioCedulas[0] = "00000";
            usuarioContraseñas[0] = "12345";
            usuarioArticulos[0] = 0;
            totalUsuarios = 1;

            Console.WriteLine(
                "\n===============================" +
                "\n=SISTEMA DE GESTIÓN DE TIENDAS=" +
                "\n===============================");

            Autenticacion();
        }

        // ─────────────────────────────────────────────────────────
        // 1. AUTENTICACIÓN
        // ─────────────────────────────────────────────────────────
        static void Autenticacion()
        {
            bool programaActivo = true;
            while (programaActivo)
            {
                bool autenticado = false;
                usuarioLogueadoIdx = -1;

                while (!autenticado)
                {
                    Console.WriteLine(
                        "\n------------------" +
                        "\n-Inicio de sesion-" +
                        "\n------------------");
                    Console.Write("Usuario (Cédula): ");
                    string cedula = Console.ReadLine();
                    Console.Write("Contraseña: ");
                    string clave = Console.ReadLine();

                    for (int i = 0; i < totalUsuarios; i++)
                    {
                        if (usuarioCedulas[i] == cedula && usuarioContraseñas[i] == clave)
                        {
                            usuarioLogueadoIdx = i;
                            break;
                        }
                    }

                    if (usuarioLogueadoIdx != -1)
                    {
                        autenticado = true;
                        Console.WriteLine($"\nBienvenido, {usuarioNombres[usuarioLogueadoIdx]}");
                    }
                    else
                    {
                        Console.WriteLine("\nUsuario y/o contraseña incorrectos.");
                    }
                }

                MenuPrincipal();
            }
        }

        // ─────────────────────────────────────────────────────────
        // 2. MENÚ PRINCIPAL
        // ─────────────────────────────────────────────────────────
        static void MenuPrincipal()
        {
            bool sesionActiva = true;
            while (sesionActiva)
            {
                Console.WriteLine(
                    "\n----------------" +
                    "\n-Menú Principal-" +
                    "\n----------------");
                Console.WriteLine("1. Gestión de usuarios");
                Console.WriteLine("2. Gestión de artículos");
                Console.WriteLine("3. Gestión de ventas");
                Console.WriteLine("4. Cerrar sesión");
                Console.Write("Seleccione una opción: ");
                string op = Console.ReadLine();

                if (op == "1") GestionarUsuarios();
                else if (op == "2") GestionarArticulos();
                else if (op == "3") GestionarVentas();
                else if (op == "4")
                {
                    sesionActiva = false;
                    Console.WriteLine("\nCerrando sesión...");
                }
                else Console.WriteLine("Ingrese una opción del menú válida.");
            }
        }

        // ─────────────────────────────────────────────────────────
        // 3. GESTIÓN DE USUARIOS
        // ─────────────────────────────────────────────────────────
        static void GestionarUsuarios()
        {
            bool menuUsuario = true;
            while (menuUsuario)
            {
                Console.WriteLine(
                    "\n---------------------" +
                    "\n-Gestión de usuarios-" +
                    "\n---------------------");
                Console.WriteLine("1. Ver lista de usuarios");
                Console.WriteLine("2. Nuevo usuario");
                Console.WriteLine("3. Editar información de usuario");
                Console.WriteLine("4. Salir de Gestión de usuarios");
                Console.Write("Seleccione una opción: ");
                string op = Console.ReadLine();

                if (op == "1")
                {
                    Console.WriteLine("\nLista de usuarios\n-----------------");
                    MostrarMenuUsuarios();
                }
                else if (op == "2")
                {
                    Console.WriteLine("\nCrear Usuario\n-------------");
                    CrearUsuario();
                }
                else if (op == "3")
                {
                    Console.WriteLine("\nEditar Usuario\n--------------");
                    MostrarMenuEdicionUsuarios();
                }
                else if (op == "4") menuUsuario = false;
                else Console.WriteLine("Ingrese una opción del menú válida.");
            }
        }

        static void CrearUsuario()
        {
            if (totalUsuarios >= MAX_USUARIOS)
            {
                Console.WriteLine("Error: capacidad máxima de usuarios alcanzada.");
                return;
            }

            Console.Write("Nombre completo: ");
            string nombre = Console.ReadLine();

            string cedula = "";
            bool cedulaDisponible = false;
            while (!cedulaDisponible)
            {
                Console.Write("Cédula: ");
                cedula = Console.ReadLine();
                if (CedulaValida(cedula)) cedulaDisponible = true;
            }

            Console.Write("Contraseña: ");
            string clave = Console.ReadLine();

            usuarioNombres[totalUsuarios] = nombre;
            usuarioCedulas[totalUsuarios] = cedula;
            usuarioContraseñas[totalUsuarios] = clave;
            usuarioArticulos[totalUsuarios] = 0;
            totalUsuarios++;

            Console.WriteLine("Usuario creado exitosamente.");
        }

        static bool CedulaValida(string cedula)
        {
            for (int i = 0; i < totalUsuarios; i++)
            {
                if (usuarioCedulas[i] == cedula)
                {
                    Console.WriteLine("Error, esta cédula ya pertenece a un usuario registrado.");
                    return false;
                }
            }
            return true;
        }

        // Muestra usuarios (sin el admin índice 0) y permite ver detalle
        static void MostrarMenuUsuarios()
        {
            if (!MostrarListaUsuarios()) return;

            int seleccion = 0;
            bool esValido = false;
            while (!esValido)
            {
                Console.Write("\nSeleccione el número del usuario que desea ver: ");
                string entrada = Console.ReadLine();
                if (int.TryParse(entrada, out seleccion) && seleccion >= 1 && seleccion < totalUsuarios)
                    esValido = true;
                else
                    Console.WriteLine("Ingrese una opción del menú válida.");
            }

            Console.WriteLine("--------------------------");
            Console.WriteLine("Usuario encontrado");
            Console.WriteLine($"Nombre: {usuarioNombres[seleccion]}");
            Console.WriteLine($"Cédula: {usuarioCedulas[seleccion]}");
            Console.WriteLine("--------------------------");
        }

        static void MostrarMenuEdicionUsuarios()
        {
            if (!MostrarListaUsuarios()) return;

            int seleccion = 0;
            bool esValido = false;
            while (!esValido)
            {
                Console.Write("Ingrese el número del usuario a editar: ");
                string entrada = Console.ReadLine();
                if (int.TryParse(entrada, out seleccion) && seleccion >= 1 && seleccion < totalUsuarios)
                    esValido = true;
                else
                    Console.WriteLine("Ingrese una opción del menú válida.");
            }

            Console.WriteLine("-------------------------------");
            Console.WriteLine("Usuario encontrado");
            Console.WriteLine($"Nombre actual: {usuarioNombres[seleccion]}");
            Console.WriteLine($"Cédula actual: {usuarioCedulas[seleccion]}");
            Console.WriteLine("-------------------------------");

            Console.Write("Ingrese nuevo nombre (Enter para mantener el actual): ");
            string nuevoNombre = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevoNombre)) usuarioNombres[seleccion] = nuevoNombre;

            bool cedulaValida = false;
            while (!cedulaValida)
            {
                Console.Write("Ingrese nueva cédula (Enter para mantener actual): ");
                string nuevaCedula = Console.ReadLine();
                if (string.IsNullOrEmpty(nuevaCedula) || nuevaCedula == usuarioCedulas[seleccion])
                    cedulaValida = true;
                else if (CedulaValida(nuevaCedula))
                {
                    usuarioCedulas[seleccion] = nuevaCedula;
                    cedulaValida = true;
                }
            }

            Console.Write("Ingrese nueva contraseña (Enter para mantener actual): ");
            string nuevaPass = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevaPass)) usuarioContraseñas[seleccion] = nuevaPass;

            Console.WriteLine("\nInformación actualizada correctamente.");
        }

        // Muestra usuarios desde índice 1 (0 = admin). Devuelve false si no hay usuarios.
        static bool MostrarListaUsuarios()
        {
            if (totalUsuarios <= 1)
            {
                Console.WriteLine("\nNo hay usuarios registrados.");
                return false;
            }
            for (int i = 1; i < totalUsuarios; i++)
                Console.WriteLine($"{i}. {usuarioCedulas[i]} - {usuarioNombres[i]}");
            return true;
        }

        // ─────────────────────────────────────────────────────────
        // 4. GESTIÓN DE ARTÍCULOS
        // ─────────────────────────────────────────────────────────
        static void GestionarArticulos()
        {
            bool menuArticulo = true;
            while (menuArticulo)
            {
                Console.WriteLine(
                    "\n----------------------" +
                    "\n-Gestión de Artículos-" +
                    "\n----------------------");
                Console.WriteLine("1. Ver lista de artículos");
                Console.WriteLine("2. Nuevo artículo");
                Console.WriteLine("3. Editar información del artículo");
                Console.WriteLine("4. Salir de Gestión de Artículos");
                Console.Write("Seleccione una opción: ");
                string op = Console.ReadLine();

                if (op == "1")
                {
                    Console.WriteLine("\nLista de artículos\n------------------");
                    MostrarMenuArticulos();
                }
                else if (op == "2")
                {
                    Console.WriteLine("\nCrear artículo\n--------------");
                    CrearArticulo();
                }
                else if (op == "3")
                {
                    Console.WriteLine("\nEditar artículo\n---------------");
                    MostrarMenuEdicionArticulos();
                }
                else if (op == "4") menuArticulo = false;
                else Console.WriteLine("Opción no válida.");
            }
        }

        static void CrearArticulo()
        {
            if (totalArticulos >= MAX_ARTICULOS)
            {
                Console.WriteLine("Error: capacidad máxima de artículos alcanzada.");
                return;
            }

            if (usuarioArticulos[usuarioLogueadoIdx] >= 5)
            {
                Console.WriteLine("\nError, has alcanzado el límite máximo de 5 artículos creados.");
                return;
            }

            // Nombre
            string nombre = "";
            bool nombreDisponible = false;
            while (!nombreDisponible)
            {
                Console.Write("Nombre: ");
                nombre = Console.ReadLine();
                if (NombreArticuloValido(nombre)) nombreDisponible = true;
            }

            // Valor
            double valorNumerico = 0;
            bool valorValido = false;
            while (!valorValido)
            {
                Console.Write("Valor unitario: ");
                string valor = Console.ReadLine();
                if (double.TryParse(valor, out valorNumerico) && valorNumerico >= 0)
                    valorValido = true;
                else
                    Console.WriteLine("Error, ingrese un número válido.");
            }

            // Cantidad
            int cantidadNumerica = 0;
            bool cantidadValida = false;
            while (!cantidadValida)
            {
                Console.Write("Unidades disponibles: ");
                string cantidad = Console.ReadLine();
                if (int.TryParse(cantidad, out cantidadNumerica) && cantidadNumerica >= 0)
                    cantidadValida = true;
                else
                    Console.WriteLine("Error, ingrese un número entero válido.");
            }

            articuloNombres[totalArticulos] = nombre;
            articuloValores[totalArticulos] = valorNumerico;
            articuloCantidades[totalArticulos] = cantidadNumerica;
            totalArticulos++;
            usuarioArticulos[usuarioLogueadoIdx]++;

            Console.WriteLine("Artículo creado exitosamente.");
        }

        static bool NombreArticuloValido(string nombreArticulo)
        {
            for (int i = 0; i < totalArticulos; i++)
            {
                if (articuloNombres[i] == nombreArticulo)
                {
                    Console.WriteLine("Error, este nombre ya pertenece a un artículo registrado.");
                    return false;
                }
            }
            return true;
        }

        static bool MostrarListaArticulos()
        {
            if (totalArticulos < 1)
            {
                Console.WriteLine("\nNo hay artículos registrados.");
                return false;
            }
            for (int i = 0; i < totalArticulos; i++)
                Console.WriteLine($"{i + 1}. {articuloNombres[i]}");
            return true;
        }

        static void MostrarMenuArticulos()
        {
            if (!MostrarListaArticulos()) return;

            int seleccion = 0;
            bool esValido = false;
            while (!esValido)
            {
                Console.Write("\nSeleccione el número del artículo que desea ver: ");
                string entrada = Console.ReadLine();
                if (int.TryParse(entrada, out seleccion) && seleccion >= 1 && seleccion <= totalArticulos)
                    esValido = true;
                else
                    Console.WriteLine("Ingrese una opción del menú válida.");
            }

            int idx = seleccion - 1;
            Console.WriteLine("--------------------------");
            Console.WriteLine("Artículo encontrado");
            Console.WriteLine($"Nombre: {articuloNombres[idx]}");
            Console.WriteLine($"Valor unitario: {articuloValores[idx]}");
            Console.WriteLine($"Unidades disponibles: {articuloCantidades[idx]}");
            Console.WriteLine("--------------------------");
        }

        static void MostrarMenuEdicionArticulos()
        {
            if (!MostrarListaArticulos()) return;

            int seleccion = 0;
            bool esValido = false;
            while (!esValido)
            {
                Console.Write("Ingrese el número del artículo a editar: ");
                string entrada = Console.ReadLine();
                if (int.TryParse(entrada, out seleccion) && seleccion >= 1 && seleccion <= totalArticulos)
                    esValido = true;
                else
                    Console.WriteLine("Ingrese una opción del menú válida.");
            }

            int idx = seleccion - 1;
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Artículo encontrado");
            Console.WriteLine($"Nombre: {articuloNombres[idx]}");
            Console.WriteLine($"Valor unitario: {articuloValores[idx]}");
            Console.WriteLine($"Unidades: {articuloCantidades[idx]}");
            Console.WriteLine("-------------------------------");

            // Editar nombre
            bool nombreValido = false;
            while (!nombreValido)
            {
                Console.Write("\nIngrese nuevo nombre (Enter para mantener el actual): ");
                string nuevoNombre = Console.ReadLine();
                if (string.IsNullOrEmpty(nuevoNombre) || nuevoNombre == articuloNombres[idx])
                    nombreValido = true;
                else if (NombreArticuloValido(nuevoNombre))
                {
                    articuloNombres[idx] = nuevoNombre;
                    nombreValido = true;
                }
            }

            // Editar valor
            bool valorValido = false;
            while (!valorValido)
            {
                Console.Write("Ingrese nuevo valor (Enter para mantener el actual): ");
                string nuevoValor = Console.ReadLine();
                if (string.IsNullOrEmpty(nuevoValor))
                    valorValido = true;
                else if (double.TryParse(nuevoValor, out double valorNumerico) && valorNumerico >= 0)
                {
                    articuloValores[idx] = valorNumerico;
                    valorValido = true;
                }
                else Console.WriteLine("Error, el valor debe ser un número válido.");
            }

            // Editar cantidad
            bool cantidadValida = false;
            while (!cantidadValida)
            {
                Console.Write("Nueva cantidad (Enter para mantener el actual): ");
                string entradaCant = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(entradaCant))
                    cantidadValida = true;
                else if (int.TryParse(entradaCant, out int nuevaCantidad) && nuevaCantidad >= 0)
                {
                    articuloCantidades[idx] = nuevaCantidad;
                    cantidadValida = true;
                }
                else Console.WriteLine("Error, el valor debe ser un número entero válido.");
            }

            Console.WriteLine("\nInformación actualizada correctamente.");
        }

        // ─────────────────────────────────────────────────────────
        // 5. GESTIÓN DE VENTAS
        // ─────────────────────────────────────────────────────────
        static void GestionarVentas()
        {
            if (totalArticulos == 0)
            {
                Console.WriteLine("\nNo hay artículos registrados en el sistema.");
                Console.WriteLine("Registre artículos antes de intentar gestionar ventas.");
                return;
            }

            bool menuVentas = true;
            while (menuVentas)
            {
                Console.WriteLine(
                    "\n--------------------" +
                    "\n-Gestión de Ventas -" +
                    "\n--------------------");
                Console.WriteLine("1. Registrar venta");
                Console.WriteLine("2. Salir de Gestión de ventas");
                Console.Write("Seleccione una opción: ");
                string op = Console.ReadLine();

                if (op == "1")
                {
                    Console.WriteLine("\nArtículos disponibles\n---------------------");
                    MostrarMenuInventario();
                }
                else if (op == "2") menuVentas = false;
                else Console.WriteLine("Ingrese una opción del menú válida.");
            }
        }

        static void MostrarMenuInventario()
        {
            // Mostrar lista con cantidades
            if (totalArticulos < 1)
            {
                Console.WriteLine("\nNo hay artículos registrados.");
                return;
            }
            for (int i = 0; i < totalArticulos; i++)
                Console.WriteLine($"{i + 1}. {articuloNombres[i]} (stock: {articuloCantidades[i]})");

            int seleccion = 0;
            bool esValido = false;
            while (!esValido)
            {
                Console.Write("\nIngrese el número del artículo que desea comprar: ");
                string entrada = Console.ReadLine();
                if (int.TryParse(entrada, out seleccion) && seleccion >= 1 && seleccion <= totalArticulos)
                    esValido = true;
                else
                    Console.WriteLine("Ingrese una opción del menú válida.");
            }

            int idx = seleccion - 1;
            Console.WriteLine("--------------------------");
            Console.WriteLine("Artículo encontrado");
            Console.WriteLine($"Nombre: {articuloNombres[idx]}");
            Console.WriteLine($"Inventario: {articuloCantidades[idx]}");
            Console.WriteLine("--------------------------");

            bool cantidadValida = false;
            while (!cantidadValida)
            {
                Console.Write("Ingrese la cantidad de unidades a comprar: ");
                string entradaCantidad = Console.ReadLine();

                if (int.TryParse(entradaCantidad, out int cantidadAComprar) && cantidadAComprar > 0)
                {
                    if (cantidadAComprar <= articuloCantidades[idx])
                    {
                        articuloCantidades[idx] -= cantidadAComprar;
                        double total = cantidadAComprar * articuloValores[idx];
                        Console.WriteLine("Venta exitosa.");
                        Console.WriteLine($"Total a pagar: {total:C}");
                        Console.WriteLine($"Nuevo inventario de {articuloNombres[idx]}: {articuloCantidades[idx]}");
                        cantidadValida = true;
                        IngresarArticuloPorVentas();
                    }
                    else
                    {
                        Console.WriteLine("Error, no hay suficientes unidades del artículo.");
                        cantidadValida = true; // salir del ciclo; la venta no se concreta
                    }
                }
                else Console.WriteLine("Error, ingrese un número entero válido mayor a 0.");
            }
        }

        static void IngresarArticuloPorVentas()
        {
            bool menu = true;
            while (menu)
            {
                Console.WriteLine("__________________________________");
                Console.WriteLine("¿Desea ingresar un nuevo artículo?");
                Console.WriteLine("1. Sí");
                Console.WriteLine("2. No");
                Console.Write("Seleccione una opción: ");
                string op = Console.ReadLine();

                if (op == "1")
                {
                    Console.WriteLine("__________________________________");
                    CrearArticulo();
                }
                else if (op == "2") menu = false;
                else Console.WriteLine("Ingrese una opción del menú válida.");
            }
        }
    }
}