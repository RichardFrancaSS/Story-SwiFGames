using SwiFGames.Entities;
using System.Globalization;
using SwiFGames.Entities.Enums;

namespace SwiFGames
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BaseUser baseUsers = new BaseUser();
            RegisterUsersInTheBaseManually(baseUsers);
            Catalog catalog = new Catalog();
            OrderHistory orderHistory = new OrderHistory();
            RegisterProductInCatalogManually(catalog);
            MainTitle();
            MainMenu(baseUsers, catalog, orderHistory);
        }
        public static void MainTitle()
        {
            Console.WriteLine(@"
░██████╗░██╗░░░░░░░██╗██╗███████╗  ░██████╗░░█████╗░███╗░░░███╗███████╗░██████╗
██╔════╝░██║░░██╗░░██║██║██╔════╝  ██╔════╝░██╔══██╗████╗░████║██╔════╝██╔════╝
╚█████╗░░╚██╗████╗██╔╝██║█████╗░░  ██║░░██╗░███████║██╔████╔██║█████╗░░╚█████╗░
░╚═══██╗░░████╔═████║░██║██╔══╝░░  ██║░░╚██╗██╔══██║██║╚██╔╝██║██╔══╝░░░╚═══██╗
██████╔╝░░╚██╔╝░╚██╔╝░██║██║░░░░░  ╚██████╔╝██║░░██║██║░╚═╝░██║███████╗██████╔╝
╚═════╝░░░░╚═╝░░░╚═╝░░╚═╝╚═╝░░░░░  ░╚═════╝░╚═╝░░╚═╝╚═╝░░░░░╚═╝╚══════╝╚═════╝░");
            Console.WriteLine("================================Seja Bem-Vindo!================================");
        }
        public static void MainMenu(BaseUser baseUsers, Catalog catalog, OrderHistory orderHistory)
        {

            Console.WriteLine();
            FormatTitles("MENU PRINCIPAL");
            Console.WriteLine("1 - Cadastre-se\n2 - Fazer Login\n");
            Console.WriteLine();
            Console.Write("Digite a opção desejada: ");
            int optionMainMenu = int.Parse(Console.ReadLine()!);

            switch (optionMainMenu)
            {
                case 1:
                    Console.Clear();
                    MainTitle();
                    UserRegistrationMenu(baseUsers);

                    Console.WriteLine();
                    Console.WriteLine("Deseja voltar para o Menu Principal? s/n");
                    char op = char.Parse(Console.ReadLine()!);
                    if (op == 's')
                    {
                        Console.Clear();
                        MainTitle();
                        MainMenu(baseUsers, catalog, orderHistory);
                    }

                    break;
                case 2:
                    Console.Clear();
                    MainTitle();
                    LoginMenu(baseUsers, catalog, orderHistory);

                    break;
                default:
                    Console.WriteLine("Esta Opção não existe no Menu, favor digitar novamente");
                    Thread.Sleep(2000);
                    Console.Clear();
                    MainTitle();
                    MainMenu(baseUsers, catalog, orderHistory);

                    break;
            }

        }
        public static void UserRegistrationMenu(BaseUser baseUsers)
        {
            Console.WriteLine();
            FormatTitles("TELA DE CADASTRO DE USUÁRIOS");
            Console.WriteLine("1 - Cliente\n2 - Administrador\n3 - Imprimir Lista de Usuarios Cadastrados");
            Console.WriteLine();
            Console.Write("Selecione a opção desejada: ");
            int op = int.Parse(Console.ReadLine()!);

            if (op == 1)
            {
                Console.Clear();
                MainTitle();
                UserRegistration(baseUsers, "Customer", op);
            }
            else if (op == 2)
            {
                Console.Clear();
                MainTitle();
                UserRegistration(baseUsers, "Administrator", op);
            }
            else if (op == 3)
            {
                Console.Clear();
                MainTitle();
                FormatTitles("Lista de Usúarios Cadastrados: ");
                Console.WriteLine(baseUsers);

            }
            else
            {
                Console.WriteLine("Esta Opção não existe no Menu, favor digitar novamente");
                Thread.Sleep(2000);
                Console.Clear();
                MainTitle();
                UserRegistrationMenu(baseUsers);
            }


        }
        public static void LoginMenu(BaseUser baseUsers, Catalog catalog, OrderHistory orderHistory)
        {
            FormatTitles("Escolha uma Opção\n1 - Fazer login \n2 - Volta ao menu Principal");
            int optionLoginMenu = int.Parse(Console.ReadLine()!);
            if (optionLoginMenu == 1)
            {
                Console.Clear();
                MainTitle();
                Console.WriteLine();
                FormatTitles("Faça seu login: ");
                Console.Write("E-mail:");
                string email = Console.ReadLine()!;
                Console.Write("Password:");
                string password = Console.ReadLine()!;

                string? administrador = "Administrator";
                string? customer = "Customer";

                int tamanhoList = baseUsers.users.Count();
                int count = 0;
                foreach (User user in baseUsers.users)
                {
                    if (user.Email == email && user.Password == password)
                    {
                        if (user.Category == administrador)
                        {
                            Console.Clear();
                            MainTitle();
                            Console.WriteLine();
                            FormatTitles("ADMINISTRATOR MENU");

                        }
                        else if (user.Category == customer)
                        {
                            Customer client = new Customer(user.UserId, user.Name, user.Email, user.Phone, user.Password, user.Category);
                            Console.Clear();
                            MainTitle();
                            Console.WriteLine();
                            FormatTitles("Customer Menu");
                            CustomerMenu(catalog, client, orderHistory);
                        }
                    }
                    else
                    {
                        count++;
                    }

                }
                if (count == tamanhoList)
                {
                    Console.WriteLine();
                    Console.WriteLine("Credenciais inválidas, Digite Novamente!");
                    Thread.Sleep(2000);
                    Console.Clear();
                    MainTitle();
                    LoginMenu(baseUsers, catalog, orderHistory);
                }
            }
            else
            {
                Console.Clear();
                MainTitle();
                MainMenu(baseUsers, catalog, orderHistory);
            }

        }
        public static void UserRegistration(BaseUser baseUsers, string category, int op)
        {
            Console.WriteLine();
            FormatTitles("ENTRE COM OS DADOS DO USUÁRIO: ");
            Console.Write("Id: ");
            int userId = int.Parse(Console.ReadLine()!);
            if (baseUsers.users.Find(x => x.UserId == userId) != null)
            {
                Console.WriteLine("Id já existente na base! Por gentileza Digitar novamente!");
                Thread.Sleep(3000);
                Console.Clear();
                MainTitle();
                UserRegistration(baseUsers, category, op);
            }

            Console.Write("Nome: ");
            string name = Console.ReadLine()!;
            Console.Write("Email: ");
            string email = Console.ReadLine()!;
            Console.Write("Phone: ");
            string phone = Console.ReadLine()!;
            Console.Write("Password: ");
            string password = Console.ReadLine()!;


            if (op == 1)
            {
                baseUsers.AddNewUserAtBase(new Customer(userId, name, email, phone, password, category));
            }
            else if (op == 2)
            {
                baseUsers.AddNewUserAtBase(new Administrator(userId, name, email, phone, password, category));
            }

            Console.Clear();
            MainTitle();
            Console.WriteLine();
            FormatTitles("Cadastrado com Sucesso");

        }
        public static void RegisterUsersInTheBaseManually(BaseUser baseUsers)
        {
            User user1 = new Customer(1, "Rick", "rick@gmail.com", "1191991-2342", "1234567", "Customer");
            User user2 = new Customer(2, "Thiago", "thiago@gmail.com", "1191291-2312", "1234567", "Customer");
            User user3 = new Customer(3, "Nicolas", "nicolas@gmail.com", "1198991-2242", "1234567", "Customer");

            User user4 = new Administrator(4, "Guilherme", "guilherme@gmail.com", "1191993-4341", "1234567", "Administrator");
            User user5 = new Administrator(5, "Ricardo", "ricardo@gmail.com", "1191791-2512", "1234567", "Administrator");
            User user6 = new Administrator(6, "Elizabeth", "elizabeth@gmail.com", "1191241-2342", "1234567", "Administrator");

            baseUsers.AddNewUserAtBase(user1);
            baseUsers.AddNewUserAtBase(user2);
            baseUsers.AddNewUserAtBase(user3);
            baseUsers.AddNewUserAtBase(user4);
            baseUsers.AddNewUserAtBase(user5);
            baseUsers.AddNewUserAtBase(user6);
        }
        public static void FormatTitles(string text)
        {
            int qtdCaracter = text.Length;

            string asterisco = string.Empty.PadLeft(qtdCaracter, '*');

            Console.WriteLine(asterisco);
            Console.WriteLine(text);
            Console.WriteLine(asterisco + "\n");
        }
        public static void AdministratorMenu(BaseUser user)
        {
            Console.Clear();

        }
        public static void CustomerMenu(Catalog catalog, Customer client, OrderHistory orderHistory)
        {
            Console.WriteLine();
            Console.WriteLine("1 - Ver Catalogo\n2 - Ver Pedidos\n3 - Histórico de Compras\n");
            Console.WriteLine();
            Console.Write("Digite a opção desejada: ");
            int optionCustomerMenu = int.Parse(Console.ReadLine()!);
            switch (optionCustomerMenu)
            {
                case 1:
                    Console.Clear();
                    MainTitle();
                    FormatTitles("CATÁLOGO DA LOJA");
                    Console.WriteLine(catalog);

                    Console.WriteLine();
                    FormatTitles("Deseja fazer um pedido? (s/n)");
                    Console.WriteLine();
                    Console.Write("Digite a opção desejada: ");
                    char op = char.Parse(Console.ReadLine()!);
                    if (op == 's')
                    {
                        RegisterOrder(catalog, client, orderHistory);
                    }
                    else if (op == 'n')
                    {
                        Console.Clear();
                        MainTitle();
                        CustomerMenu(catalog, client, orderHistory);
                    }
                    break;
                case 2:
                    Console.Clear();
                    MainTitle();
                    Console.WriteLine();
                    FormatTitles("Dados do Seu Pedido");
                    Console.WriteLine(orderHistory);
                    break;
            }
        }
        public static void RegisterProductInCatalogManually(Catalog catalog)
        {
            Product p1 = new Product(1, "God Of War 1", "God 4 Para todos os amantes de Jogos Nordicos", 550.00, 1);
            Product p2 = new Product(2, "The Last of US", "Zumbis e Terror", 600.00, 1);
            Product p3 = new Product(3, "Resident Evill Village", "MEDO MEDO MEDO MEDO MEDO MEDO MEDO!!!", 700.00, 1);
            Product p4 = new Product(4, "Fifa 24", "Futebol de cria", 800.00, 1);
            catalog.AddToTheCatalog(p1);
            catalog.AddToTheCatalog(p2);
            catalog.AddToTheCatalog(p3);
            catalog.AddToTheCatalog(p4);
        }
        public static void RegisterOrder(Catalog catalog, Customer client, OrderHistory orderHistory)
        {
            char controle;
            Console.Write("Deseja selecionar um produto da lista? (s/n): ");
            Console.WriteLine();
            Console.Write("Selecione a opção desejada: ");
            controle = char.Parse(Console.ReadLine()!);
            Product f;
            Order order = new Order();
            while (controle == 's')
            {

                Console.WriteLine();
                Console.Write("Digite o ID do Produto escolhido: ");
                int idproduct = int.Parse(Console.ReadLine()!);

                f = catalog.products.FirstOrDefault(x => x.ProductId == idproduct)!;
                Console.WriteLine();
                Console.Write("Digite a quantidade do produto: ");
                int quantity = int.Parse(Console.ReadLine()!);
                if (f != null)
                {
                    order.Products.Add(new Product(f.ProductId, f.Name, f.Description, f.Price * quantity, f.Quantity = quantity)); ;
                }

                Console.WriteLine();
                Console.Write("Deseja selecionar outro produto da lista? (s/n): ");
                controle = char.Parse(Console.ReadLine()!);

            }
            FormatTitles("Aguarde um instante que estamos Finalizando seu Pedido");
            StatusOrder status = Enum.Parse<StatusOrder>("Processing");
            Random aleatorio = new Random();
                int auxId = aleatorio.Next(100);

            while (orderHistory.orders.Find(x => x.OrderId == auxId) != null)
            {
                auxId = aleatorio.Next(100);
            }
                order.OrderId = auxId;
                order.Moment = DateTime.Now;
                order.Status = status;
                orderHistory.AddOrder(order);
                Thread.Sleep(4000);
                FormatTitles("Pedido Realizado com Sucesso");
                Thread.Sleep(3000);
                Console.Clear();
                MainTitle();
                Console.WriteLine();
                CustomerMenu(catalog, client, orderHistory);

            
        }
    }

}