using System;
namespace GestionTiendas 
{
    class Usuario
    {
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public string Contraseña { get; set; }
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
                Usuario usuarioActual = null;

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

                    usuarioActual = listaUsuarios.Find(u => u.Cedula == usuario && u.Contraseña == contraseña);

                    if (usuarioActual != null)
                    {
                        autenticado = true;
                        Console.WriteLine($"\nBienvenido, {usuarioActual.Nombre}");
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
                Console.WriteLine("4. Salir del programa (cerrar sesion)");
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
                        Console.WriteLine("Opción inválida.");
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
                        if (listaUsuarios.Count == 0) Console.WriteLine("No hay usuarios.");
                        foreach (var u in listaUsuarios)
                        {
                            Console.WriteLine($"CC: {u.Cedula} | Nombre: {u.Nombre}");
                        }
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
                        Console.Write("Ingrese el número de cédula del usuario a editar: ");
                        string cedulaBusqueda = Console.ReadLine();

                        Usuario usuarioAEditar = listaUsuarios.Find(u => u.Cedula == cedulaBusqueda);

                        if (usuarioAEditar != null)
                        {
                            Console.WriteLine("Usuario encontrado");
                            Console.WriteLine($"Nombre actual: {usuarioAEditar.Nombre}");
                            Console.WriteLine($"Cédula actual: {usuarioAEditar.Cedula}");
                            Console.WriteLine("--------------------------");

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

                            Console.WriteLine("\nInformación actualizada correctamente.");
                        }
                        else
                        {
                            Console.WriteLine("Usuario no encontrado.");
                        }
                        break;

                    case "4":
                        menuUsuario = false;
                        break;

                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
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

        // 4. Gestión de articulos
        static void GestionarArticulos()
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
                        if (listaArticulos.Count == 0) Console.WriteLine("No hay artículos");
                        foreach (var art in listaArticulos)
                        {
                            Console.WriteLine($"Nombre:{art.Nombre} | Valor unitario: {art.Valor}");
                        }
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
                        Console.Write("Ingrese el nombre del artículo a editar:");
                        string nombreArticulo = Console.ReadLine();

                        Articulo articuloAEditar = listaArticulos.Find(a => a.Nombre == nombreArticulo);

                        if (articuloAEditar != null)
                        {
                            Console.WriteLine("Artículo encontrado:");
                            Console.WriteLine($"Nombre actual: {articuloAEditar.Nombre}");
                            Console.WriteLine($"Valor actual: {articuloAEditar.Valor}");
                            Console.WriteLine("-----------------------");

                            bool nombreValido = false;
                            while (!nombreValido) {
                                Console.Write("\nIngrese nuevo nombre(Enter para mantener el actual): ");
                                string nuevoNombre = Console.ReadLine();
                                if (string.IsNullOrEmpty(nuevoNombre) || nuevoNombre == articuloAEditar.Nombre)
                                {
                                    nombreValido = true;
                                }
                                else 
                                {
                                    if (NombreArticuloValido(nuevoNombre)) {
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

                            Console.Write("\nInformación actualizada correctamente.");
                        }
                        else
                        {
                            Console.WriteLine("Artículo no encontrado.");
                        }
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

        static void CrearArticulo()
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


            articulo.Cantidad = 0;

            listaArticulos.Add(articulo);
            Console.WriteLine("Artículo creado exitosamente");
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
            Console.WriteLine(
                "\nRegistro Inicial de Inventario" +
                "\n------------------------------");
            Console.Write("Ingrese el nombre del artículo al que desea realizar inventario:");
            string nombreArticulo = Console.ReadLine();

            Articulo articuloAEditar = listaArticulos.Find(a => a.Nombre == nombreArticulo);

            if (articuloAEditar != null)
            {
                Console.WriteLine("Artículo encontrado");
                Console.WriteLine($"Nombre: {articuloAEditar.Nombre}");
                Console.WriteLine($"Cantidad de unidades disponibles: {articuloAEditar.Cantidad}");
                Console.WriteLine("---------------------------------------");

                
                Console.Write("\nIngrese la cantidad de unidades disponibles (Enter para mantener el actual): ");
                string nuevaCantidad = Console.ReadLine();
                //if (!string.IsNullOrEmpty(nuevaCantidad)) articuloAEditar.Cantidad = int.Parse(nuevaCantidad);
                if (!string.IsNullOrEmpty(nuevaCantidad))
                {
                    int cantidadNumerica;
                    while (!int.TryParse(nuevaCantidad, out cantidadNumerica))
                    {
                        Console.WriteLine("Error, ingrese un número válido.");
                        Console.Write("Ingrese la cantidad de unidades disponibles (Enter para mantener el actual): ");
                        nuevaCantidad = Console.ReadLine();
                        if (string.IsNullOrEmpty(nuevaCantidad)) break;
                    }

                    if (int.TryParse(nuevaCantidad, out cantidadNumerica))
                    {
                        articuloAEditar.Cantidad = cantidadNumerica;
                    }
                }

                Console.WriteLine("Información actualizada correctamente.");
            }
            else
            {
                Console.WriteLine("Artículo no encontrado.");
            }

            bool menuVentas = true;
            while (menuVentas)
            {
                Console.WriteLine(
                    "\n--------------------" +
                    "\n-Gestión de Ventas -" +
                    "\n--------------------");
                Console.WriteLine("1. Registrar venta");
                Console.WriteLine("2. Salir de Gestión de venta (Menú Principal)");
                Console.Write("Seleccione una opción: ");
                string op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        Console.WriteLine("" +
                            "\nArtículos disponibles" +
                            "\n---------------------");
                        foreach (var a in listaArticulos)
                        {
                            Console.WriteLine($"- {a.Nombre} ({a.Cantidad})");
                        }

                        Console.Write("\nIngrese el nombre del artículo que desea vender: ");
                        string nombreBusqueda = Console.ReadLine();

                        Articulo articuloSeleccionado = listaArticulos.Find(a => a.Nombre.ToLower() == nombreBusqueda.ToLower());

                        if (articuloSeleccionado != null)
                        {
                            Console.WriteLine($"\nArtículo: {articuloSeleccionado.Nombre}");
                            Console.WriteLine($"Inventario actual: {articuloSeleccionado.Cantidad}");

                            Console.Write("Ingrese la cantidad de unidades a comprar: ");
                            string entradaCantidad = Console.ReadLine();

                            if (int.TryParse(entradaCantidad, out int cantidadAComprar))
                            {
                                if (cantidadAComprar <= articuloSeleccionado.Cantidad)
                                {
                                    Console.WriteLine("Usted puede comprar el artículo");

                                    articuloSeleccionado.Cantidad -= cantidadAComprar;

                                    Console.WriteLine("Venta exitosa.");
                                    Console.WriteLine($"Nuevo inventario de {articuloSeleccionado.Nombre}: {articuloSeleccionado.Cantidad}");
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
                        else
                        {
                            Console.WriteLine("Artículo no encontrado.");
                        }
                        break;

                    case "2":
                        menuVentas = false;
                        break;

                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            }
        }
    }
}