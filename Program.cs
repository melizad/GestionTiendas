using System;
namespace GestionTiendas 
{
    class Usuario
    {
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public string Contraseña { get; set; }
        public int ArticulosIngresado { get; set; } = 0;
    }

    class Articulo
    {
        public string Nombre { get; set; }
        public double Valor { get; set; }
        public int Cantidad { get; set; }
    }

    class Program
    {
        public static List<Usuario> listaUsuarios = new List<Usuario>();
        public static List<Articulo> listaArticulos = new List<Articulo>();
        public static Usuario usuarioLogueado = new Usuario();
        static void Main(string[] args)
        {
            // Usuario por defecto
            listaUsuarios.Add(new Usuario { Nombre = "Administrador", Cedula = "00000", Contraseña = "12345" });

            bool programaActivo = true;
            Console.WriteLine(
                "\n===============================" +
                "\n=SISTEMA DE GESTIÓN DE TIENDAS=" +
                "\n===============================");
            Autenticacion(programaActivo);
        }

        // 1. Autenticación
        static void Autenticacion(bool programaActivo)
        {
            while (programaActivo)
            {
                
                bool autenticado = false;
                usuarioLogueado = null;

                while (!autenticado)
                {
                    Console.WriteLine(
                        "\n------------------" +
                        "\n-Inicio de sesion-" +
                        "\n------------------");
                    Console.Write("Usuario (Cédula): ");
                    string usuario = Console.ReadLine();
                    Console.Write("Contraseña: ");
                    string contraseña = Console.ReadLine();

                    usuarioLogueado = listaUsuarios.Find(u => u.Cedula == usuario && u.Contraseña == contraseña);

                    if (usuarioLogueado != null)
                    {
                        autenticado = true;
                        Console.WriteLine($"\nBienvenido, {usuarioLogueado.Nombre}");
                    }
                    else
                    {
                        Console.WriteLine("\nUsuario y/o contraseña incorrectos.");
                    }
                }

                bool sesionActiva = true;
                MenuPrincipal(sesionActiva);

            }
        }

        // 2. Menú Principal
        static void MenuPrincipal(bool sesionActiva)
        {
            while (sesionActiva)
            {
                Console.WriteLine(
                    "\n----------------" +
                    "\n-Menú Principal-" +
                    "\n----------------");
                Console.WriteLine("1. Gestión de usuarios");
                Console.WriteLine("2. Gestión de artículos");
                Console.WriteLine("3. Gestión de ventas");
                Console.WriteLine("4. Salir del programa");
                Console.Write("Seleccione una opción: ");
                string op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        GestionarUsuarios();
                        break;
                    case "2":
                        GestionarArticulos();
                        break;
                    case "3":
                        GestionarVentas();
                        break;
                    case "4":
                        sesionActiva = false;
                        Console.WriteLine("\nCerrando sesión...");
                        break;
                    default:
                        Console.WriteLine("Ingrese una opción del menú válida.");
                        break;
                }
            }
        }

        //3. Gestión de usuario
        static void GestionarUsuarios()
        {
            bool menuUsuario = true;
            while (menuUsuario)
            {
                //RegistroInicialUsuarios();
                Console.WriteLine(
                    "\n---------------------" +
                    "\n-Gestión de usuarios-" +
                    "\n---------------------");
                Console.WriteLine("1. Ver lista de usuarios");
                Console.WriteLine("2. Nuevo usuario");
                Console.WriteLine("3. Editar información de usuario");
                Console.WriteLine("4. Salir de Gestión de usuarios");
                Console.Write("Seleccione una opción: ");
                string subOp = Console.ReadLine();

                switch (subOp)
                {
                    case "1":
                        Console.WriteLine(
                            "\nLista de usuarios" +
                            "\n-----------------");
                        MostrarMenuUsuarios();
                        break;

                    case "2":
                        Console.WriteLine(
                            "\nCrear Usuario" +
                            "\n-------------");
                        CrearUsuario();
                        break;

                    case "3":
                        Console.WriteLine(
                            "\nEditar Usuario" +
                            "\n--------------");
                        MostrarMenuEdicionUsuarios();
                        break;

                    case "4":
                        menuUsuario = false;
                        break;

                    default:
                        Console.WriteLine("Ingrese una opción del menú válida.");
                        break;
                }
            }
        }
        static void RegistroInicialUsuarios() {
            
            if (listaUsuarios.Count == 1)
            {
                Console.WriteLine(
                    "\nRegistro Inicial de Usuarios" +
                    "\n-----------------------------");
                for (int i = 1; i <= 5; i++)
                {
                    Console.WriteLine("Usuario " + i);
                    CrearUsuario();
                    Console.WriteLine();
                }
            }   
        }

        static void CrearUsuario()
        {
            Usuario usuario = new Usuario();

            Console.Write("Nombre completo: ");
            usuario.Nombre = Console.ReadLine();

            string cedula = "";
            bool cedulaDisponible = false;
            while (!cedulaDisponible)
            {
                Console.Write("Cédula: ");
                cedula = Console.ReadLine();

                if (CedulaValida(cedula))
                {
                    cedulaDisponible = true; 
                }
            }
            usuario.Cedula = cedula;

            Console.Write("Contraseña: ");
            usuario.Contraseña = Console.ReadLine();

            listaUsuarios.Add(usuario);
            Console.WriteLine("Usuario creado exitosamente");
        }

        static bool CedulaValida(string cedula)
        {
            if (listaUsuarios.Any(u => u.Cedula == cedula))
            {
                Console.WriteLine("Error, esta cédula ya pertenece a un usuario registrado");
                return false;
            }
            return true;
        }
        static void MostrarMenuUsuarios()
        {
            bool menuUsuario = true;
            while (menuUsuario)
            { 
                if (!MostrarListaUsuarios()) {
                    GestionarUsuarios();
                }
            
                int seleccion = 0;
                bool esValido = false;

                while (!esValido)
                {
                    Console.Write("\nSeleccione el número del usuario que desea ver: ");
                    string entrada = Console.ReadLine();

                    if (int.TryParse(entrada, out seleccion) && seleccion >= 1 && seleccion < listaUsuarios.Count)
                    {
                        esValido = true;
                    }
                    else
                    {
                        Console.WriteLine("Ingrese una opción del menú válida.");
                    }
                }
                Usuario usuarioDetalle = listaUsuarios[seleccion];

                Console.WriteLine("--------------------------");
                Console.WriteLine("Usuario encontrado");
                Console.WriteLine($"Nombre: {usuarioDetalle.Nombre}");
                Console.WriteLine($"Cédula: {usuarioDetalle.Cedula}");
                Console.WriteLine("--------------------------");
                menuUsuario = false;
            }
        }

        static void MostrarMenuEdicionUsuarios()
        {
            if (!MostrarListaUsuarios()) {
                GestionarUsuarios();
            }
            
            Console.WriteLine();
            int seleccion = 0;
            bool esValido = false;

            while (!esValido)
            {
                Console.Write("Ingrese el número del usuario a editar: ");
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out seleccion) && seleccion >= 1 && seleccion <= listaUsuarios.Count)
                {
                    esValido = true;
                }
                else
                {
                    Console.WriteLine("Ingrese una opción del menú válida.");
                }
            }
            Usuario usuarioAEditar = listaUsuarios[seleccion];
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Usuario encontrado");
            Console.WriteLine($"Nombre actual: {usuarioAEditar.Nombre}");
            Console.WriteLine($"Cédula actual: {usuarioAEditar.Cedula}");
            Console.WriteLine("-------------------------------");
            Console.Write("Ingrese nuevo nombre (Enter para mantener el actual): ");

            string nuevoNombre = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevoNombre)) usuarioAEditar.Nombre = nuevoNombre;
            bool cedulaValida = false;

            while (!cedulaValida)
            {
                Console.Write("Ingrese nueva cédula (Enter para mantener actual): ");
                string nuevaCedula = Console.ReadLine();

                if (string.IsNullOrEmpty(nuevaCedula) || nuevaCedula == usuarioAEditar.Cedula)
                {
                    cedulaValida = true;
                }
                else
                {
                    if (CedulaValida(nuevaCedula))
                    {
                        usuarioAEditar.Cedula = nuevaCedula;
                        cedulaValida = true;
                    }

                }
            }
            Console.Write("Ingrese nueva contraseña (Enter para mantener actual): ");
            string nuevaPass = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevaPass)) usuarioAEditar.Contraseña = nuevaPass;
            Console.WriteLine("\nInformación actualizada correctamente");
        }


        static bool MostrarListaUsuarios()
        {
            if (listaUsuarios.Count <= 1)
            {
                Console.WriteLine("\nNo hay usuarios registrados.");
                return false;
            }

            for (int i = 1; i < listaUsuarios.Count; i++)
            {
                Console.WriteLine($"{i}. {listaUsuarios[i].Cedula}");
                return true;
            }
            return false;
        }

        // 4. Gestión de articulos
        static void GestionarArticulos()
        {
            //RegistroInicialArticulos();
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
                string subOp = Console.ReadLine();

                switch (subOp)
                {
                    case "1": 
                        Console.WriteLine(
                            "\nLista de artículos" +
                            "\n------------------");
                        MostrarMenuArticulos();
                        break;

                    case "2": 
                        Console.WriteLine(
                            "\nCrear artículo" +
                            "\n--------------");
                        CrearArticulo();
                        break;

                    case "3":
                        Console.WriteLine(
                            "\nEditar artículo" +
                            "\n---------------");
                        MostrarMenuEdicionArticulos();
                        break;

                    case "4":
                        menuArticulo = false;
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }

        static void RegistroInicialArticulos()
        {
            if (listaArticulos.Count == 0)
            {
                Console.WriteLine(
                    "\nRegistro Inicial de Artículos" +
                    "\n-----------------------------");
                for (int i = 1; i <= 5; i++)
                {
                    Console.WriteLine($"Artículo {i}:");
                    CrearArticulo();
                    Console.WriteLine();

                }
            }
        }

        static void MostrarMenuArticulos()
        {
            bool exixstenArticulos = MostrarListaArticulos();
            if (!exixstenArticulos) {
                GestionarArticulos();
            }
            int seleccion = 0;
            bool esValido = false;

            while (!esValido)
            {
                Console.Write("\nSeleccione el número del articulo que desea ver: ");
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out seleccion) && seleccion >= 1 && seleccion <= listaArticulos.Count)
                {
                    esValido = true;
                }
                else
                {
                    Console.WriteLine("Ingrese una opción del menú válida.");
                }
            }
            Articulo articuloDetalle = listaArticulos[seleccion-1];

            Console.WriteLine("--------------------------");
            Console.WriteLine("Articulo encontrado");
            Console.WriteLine($"Nombre: {articuloDetalle.Nombre}");
            Console.WriteLine($"Valor Unitario: {articuloDetalle.Valor}");
            Console.WriteLine("--------------------------");

        }

        static bool MostrarListaArticulos()
        {
            if (listaArticulos.Count < 1)
            {
                Console.WriteLine("\nNo hay artículos registrados.");
                return false;
            }

            for (int i = 0; i < listaArticulos.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {listaArticulos[i].Nombre}");
            }
            return true;
        }

        static void MostrarMenuEdicionArticulos()
        {
            bool exixstenArticulos = MostrarListaArticulos();
            if (!exixstenArticulos)
            {
                GestionarArticulos();
            }
            Console.WriteLine();
            int seleccion = 0;

            while (true)
            {
                Console.Write("Ingrese el número del artículo a editar: ");
                if (int.TryParse(Console.ReadLine(), out seleccion) && seleccion >= 1 && seleccion <= listaArticulos.Count)
                {
                    break;
                }
                Console.WriteLine("Ingrese una opción del menú válida.");
            }

            Articulo articuloAEditar = listaArticulos[seleccion-1];
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Artículo encontrado");
            Console.WriteLine($"Nombre: {articuloAEditar.Nombre}");
            Console.WriteLine($"Valor unitario : {articuloAEditar.Valor}");
            Console.WriteLine("-------------------------------");

            bool nombreValido = false;
            while (!nombreValido)
            {
                Console.Write("\nIngrese nuevo nombre(Enter para mantener el actual): ");
                string nuevoNombre = Console.ReadLine();
                if (string.IsNullOrEmpty(nuevoNombre) || nuevoNombre == articuloAEditar.Nombre)
                {
                    nombreValido = true;
                }
                else
                {
                    if (NombreArticuloValido(nuevoNombre))
                    {
                        articuloAEditar.Nombre = nuevoNombre;
                        nombreValido = true;
                    }
                }
            }

            Console.Write("Ingrese nuevo valor (Enter para mantener el actual): ");
            string nuevoValor = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevoValor))
            {
                double valorNumerico;
                while (!double.TryParse(nuevoValor, out valorNumerico))
                {
                    Console.WriteLine("Error, el valor debe ser un número válido.");
                    Console.Write("Ingrese nuevo valor (Enter para mantener el actual): ");
                    nuevoValor = Console.ReadLine();

                    if (string.IsNullOrEmpty(nuevoValor)) break;
                }

                if (double.TryParse(nuevoValor, out valorNumerico))
                {
                    articuloAEditar.Valor = valorNumerico;
                }
            }

            while (true)
            {
                Console.Write("Nueva cantidad (Enter para mantener el actual): ");
                string entradaCant = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(entradaCant)) break;

                if (int.TryParse(entradaCant, out int nuevaCantidad) && nuevaCantidad >= 0)
                {
                    articuloAEditar.Cantidad = nuevaCantidad;
                    break;
                }
                Console.WriteLine("Error, el valor debe ser un número válido.");
            }

            Console.Write("\nInformación actualizada correctamente.");
        }

        static void CrearArticulo()
        {
            if (usuarioLogueado.ArticulosIngresado < 5)
            {
                Articulo articulo = new Articulo();

                string nombre = "";
                bool nombreDisponible = false;
                while (!nombreDisponible)
                {
                    Console.Write("Nombre: ");
                    nombre = Console.ReadLine();

                    if (NombreArticuloValido(nombre))
                    {
                        nombreDisponible = true;
                    }
                }
                articulo.Nombre = nombre;

                Console.Write("Valor unitario: ");
                string valor = Console.ReadLine();

                if (!string.IsNullOrEmpty(valor))
                {
                    double valorNumerico;
                    while (!double.TryParse(valor, out valorNumerico))
                    {
                        Console.WriteLine("Error, ingrese un número válido.");
                        Console.Write("Valor unitario: ");
                        valor = Console.ReadLine();

                        if (string.IsNullOrEmpty(valor)) break;
                    }

                    if (double.TryParse(valor, out valorNumerico))
                    {
                        articulo.Valor = valorNumerico;
                    }
                }

                Console.Write("Unidades disponibles: ");
                string cantidad = Console.ReadLine();

                if (!string.IsNullOrEmpty(cantidad))
                {
                    int catidadNumerica;
                    while (!int.TryParse(cantidad, out catidadNumerica))
                    {
                        Console.WriteLine("Error, ingrese un número válido.");
                        Console.Write("Unidades disponibles: ");
                        cantidad = Console.ReadLine();

                        if (string.IsNullOrEmpty(cantidad)) break;
                    }

                    if (int.TryParse(cantidad, out catidadNumerica))
                    {
                        articulo.Cantidad = catidadNumerica;
                    }
                }

                listaArticulos.Add(articulo);
                usuarioLogueado.ArticulosIngresado++;
                Console.WriteLine("Artículo creado exitosamente");
            }
            else
            {
                Console.WriteLine("\nError, Has alcanzado el límite máximo de 5 artículos creados.");
            }
            
        }

        static bool NombreArticuloValido(string nombreArticulo)
        {
            if (listaArticulos.Any(a => a.Nombre == nombreArticulo))
            {
                Console.WriteLine("Error, esta nombre ya pertenece a un artículo registrado");
                return false;
            }
            return true;
        }

        // 5. Gestion de ventas
        static void GestionarVentas()
        {
            if (listaArticulos.Count == 0)
            {
                Console.WriteLine("\nNo hay artículos registrados en el sistema.");
                Console.WriteLine("Registre artículos antes de intentar gestionar ventas.");
                return; 
            }
            //RegistroInicialInventario();
           
            bool menuVentas = true;
            while (menuVentas)
            {
                Console.WriteLine(
                    "\n--------------------" +
                    "\n-Gestión de Ventas -" +
                    "\n--------------------");
                Console.WriteLine("1. Registrar venta");
                Console.WriteLine("2. Salir de Gestión de venta");

                Console.Write("Seleccione una opción: ");
                string op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        Console.WriteLine("" +
                            "\nArtículos disponibles" +
                            "\n---------------------");
                        MostrarMenuInventario();
                        break;

                    case "2":
                        menuVentas = false;
                        break;

                    default:
                        Console.WriteLine("Ingrese una opción del menú válida.");
                        break;
                }
            }
        }

        static void RegistroInicialInventario() {
            
            Console.WriteLine(
                "\nRegistro Inicial de Inventario" +
                "\n------------------------------");
            MostrarListaInventario();

            int seleccion = 0;
            bool esValido = false;

            while (!esValido)
            {
                Console.Write("\nIngrese el número del artículo al que desea realizar inventario: ");
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out seleccion) && seleccion >= 1 && seleccion <= listaArticulos.Count)
                {
                    esValido = true;
                }
                else
                {
                    Console.WriteLine("Ingrese una opción del menú válida.");
                }
            }

            Articulo articuloAEditar = listaArticulos[seleccion - 1];

            Console.WriteLine("\nArtículo encontrado");
            Console.WriteLine($"Nombre: {articuloAEditar.Nombre}");
            Console.WriteLine($"Cantidad de unidades disponibles: {articuloAEditar.Cantidad}");
            Console.WriteLine("---------------------------------------");

            Console.Write("Ingrese la cantidad de unidades disponibles (Enter para mantener el actual): ");
            string nuevaCantidad = Console.ReadLine();

            if (!string.IsNullOrEmpty(nuevaCantidad))
            {
                int cantidadNumerica;
                while (!int.TryParse(nuevaCantidad, out cantidadNumerica) || cantidadNumerica < 0)
                {
                    Console.WriteLine("Error, ingrese un número entero válido (positivo).");
                    Console.Write("Ingrese la cantidad de unidades disponibles: ");
                    nuevaCantidad = Console.ReadLine();
                    if (string.IsNullOrEmpty(nuevaCantidad)) break;
                }

                if (int.TryParse(nuevaCantidad, out cantidadNumerica))
                {
                    articuloAEditar.Cantidad = cantidadNumerica;
                    Console.WriteLine("Información actualizada correctamente.");
                }
            }
            else
            {
                Console.WriteLine("No se realizaron cambios en el inventario.");
            }
        }

        static void MostrarMenuInventario()
        {
            if (!MostrarListaArticulos())
            {
                GestionarVentas();
            }
            int seleccion = 0;
            bool esValido = false;

            while (!esValido)
            {
                Console.Write("\nIngrese el número del artículo que desea comprar: ");
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out seleccion) && seleccion >= 1 && seleccion <= listaArticulos.Count)
                {
                    esValido = true;
                }
                else
                {
                    Console.WriteLine("Ingrese una opción del menú válida.");
                }
            }
            Articulo articuloDetalle = listaArticulos[seleccion - 1];

            Console.WriteLine("--------------------------");
            Console.WriteLine("Artículo encontrado");
            Console.WriteLine($"Nombre: {articuloDetalle.Nombre}");
            Console.WriteLine($"Inventario: {articuloDetalle.Cantidad}");
            Console.WriteLine("--------------------------");

            Console.Write("Ingrese la cantidad de unidades a comprar: ");
            string entradaCantidad = Console.ReadLine();

            if (int.TryParse(entradaCantidad, out int cantidadAComprar))
            {
                if (cantidadAComprar <= articuloDetalle.Cantidad)
                {
                    Console.WriteLine("Usted puede comprar el artículo");

                    articuloDetalle.Cantidad -= cantidadAComprar;

                    Console.WriteLine("Venta exitosa.");
                    Console.WriteLine($"Nuevo inventario de {articuloDetalle.Nombre}: {articuloDetalle.Cantidad}");
                    IngresarArticuloPorVentas();
                    
                }
                else
                {
                    Console.WriteLine("Error, no hay suficientes unidades del artículo.");
                }
            }
            else
            {
                Console.WriteLine("Error, ingrese un número válido.");
            }

        }

        static void MostrarListaInventario()
        {
            if (listaArticulos.Count < 1)
            {
                Console.WriteLine("\nNo hay articulos registrados.");
                return;
            }

            for (int i = 0; i < listaArticulos.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {listaArticulos[i].Nombre} ({(listaArticulos[i].Cantidad)})");
            }
        }

        static void IngresarArticuloPorVentas() {
            bool menuArticuloPorVenta = true;
            while (menuArticuloPorVenta)
            {

                Console.WriteLine("__________________________________");
                Console.WriteLine("¿Desea ingresar un nuevo artículo?");

                Console.WriteLine("1. Si");
                Console.WriteLine("2. No");
                Console.Write("Seleccione una opción: ");
                string op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        Console.WriteLine("__________________________________");
                        CrearArticulo();
                        break;
                    case "2":
                        menuArticuloPorVenta = false;
                        break;
                    default:
                        Console.WriteLine("Ingrese una opción del menú válida.");
                        break;
                }
            }
        }
    }
}