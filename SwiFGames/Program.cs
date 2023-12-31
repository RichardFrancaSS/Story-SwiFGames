﻿using SwiFGames.Entities;
using System.Globalization;
using SwiFGames.Entities.Enums;
using System.Diagnostics;
using System.Xml.Linq;
using SwiFGames.Controlers;
using System.Xml.Schema;
using System.Linq;

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
            Console.Clear();
            Console.WriteLine(@"
█████████████████████████████████▀███████████████████████████
█─▄▄▄▄█▄─█▀▀▀█─▄█▄─▄█▄─▄▄─███─▄▄▄▄██▀▄─██▄─▀█▀─▄█▄─▄▄─█─▄▄▄▄█
█▄▄▄▄─██─█─█─█─███─███─▄█████─██▄─██─▀─███─█▄█─███─▄█▀█▄▄▄▄─█
▀▄▄▄▄▄▀▀▄▄▄▀▄▄▄▀▀▄▄▄▀▄▄▄▀▀▀▀▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▄▀▄▄▄▀▄▄▄▄▄▀▄▄▄▄▄▀");
            FormatTitles("================================Seja Bem-Vindo!================================");
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

                    MainTitle();
                    UserRegistrationMenu(baseUsers, catalog, orderHistory);

                    Console.WriteLine();
                    Console.WriteLine("Deseja voltar para o Menu Principal? s/n");
                    char op = char.Parse(Console.ReadLine()!);
                    if (op == 's')
                    {

                        MainTitle();
                        MainMenu(baseUsers, catalog, orderHistory);
                    }

                    break;
                case 2:

                    MainTitle();
                    LoginMenu(baseUsers, catalog, orderHistory);

                    break;
                default:
                    Console.WriteLine("Esta Opção não existe no Menu, favor digitar novamente");
                    Thread.Sleep(2000);

                    MainTitle();
                    MainMenu(baseUsers, catalog, orderHistory);

                    break;
            }

        }
        public static void UserRegistrationMenu(BaseUser baseUsers, Catalog catalog, OrderHistory orderHistory)
        {
            Console.WriteLine();
            FormatTitles("TELA DE CADASTRO DE USUÁRIOS");
            Console.WriteLine("1 - Cliente\n2 - Administrador\n3 - Imprimir Lista de Usuarios Cadastrados");
            Console.WriteLine();
            Console.Write("Selecione a opção desejada: ");
            int op = int.Parse(Console.ReadLine()!);

            if (op == 1)
            {

                MainTitle();
                UserRegistration(baseUsers, catalog, orderHistory, "Customer", op);
            }
            else if (op == 2)
            {

                MainTitle();
                UserRegistration(baseUsers, catalog, orderHistory, "Administrator", op);
            }
            else
            {
                Console.WriteLine("Esta Opção não existe no Menu, favor digitar novamente");
                Thread.Sleep(2000);

                MainTitle();
                UserRegistrationMenu(baseUsers, catalog, orderHistory);
            }


        }
        public static void LoginMenu(BaseUser baseUsers, Catalog catalog, OrderHistory orderHistory)
        {
            FormatTitles("Escolha uma Opção\n1 - Fazer login \n2 - Volta ao menu Principal");
            int optionLoginMenu = int.Parse(Console.ReadLine()!);
            if (optionLoginMenu == 1)
            {

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

                            Administrator administrator = new Administrator(user.UserId, user.Name, user.Email, user.Phone, user.Password, user.Category);

                            MainTitle();
                            Console.WriteLine();
                            AdministratorMenu(baseUsers, catalog, orderHistory, administrator);

                        }
                        else if (user.Category == customer)
                        {
                            Customer client = new Customer(user.UserId, user.Name, user.Email, user.Phone, user.Password, user.Category);

                            MainTitle();
                            Console.WriteLine();
                            CustomerMenu(baseUsers, catalog, orderHistory, client);
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

                    MainTitle();
                    LoginMenu(baseUsers, catalog, orderHistory);
                }
            }
            else
            {

                MainTitle();
                MainMenu(baseUsers, catalog, orderHistory);
            }

        }
        public static void UserRegistration(BaseUser baseUsers, Catalog catalog, OrderHistory orderHistory, string category, int op)
        {
            Console.WriteLine();
            FormatTitles("ENTRE COM OS DADOS DO USUÁRIO: ");

            Random aleatorio = new Random();
            int auxId = aleatorio.Next(100);

            while (baseUsers.users.FirstOrDefault(x => x.UserId == auxId) != null)
            {
                auxId = aleatorio.Next(100);
            }
            Console.WriteLine();
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
                baseUsers.AddNewUserAtBase(new Customer(auxId, name, email, phone, password, category));
            }
            else if (op == 2)
            {
                baseUsers.AddNewUserAtBase(new Administrator(auxId, name, email, phone, password, category));
            }


            Thread.Sleep(2000);
            Console.WriteLine();
            FormatTitles("Cadastrado com Sucesso");
            Thread.Sleep(3000);
            MainTitle();
            MainMenu(baseUsers, catalog, orderHistory);
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
        public static void AdministratorMenu(BaseUser baseUsers, Catalog catalog, OrderHistory orderHistory, Administrator administrator)
        {
            FormatTitles("Menu do Administrador");
            Console.WriteLine();
            Console.WriteLine("1 - Produtos\n2 - Relatórios\n3 - Usuários Cadastrados\n4 - Logout\n");
            Console.WriteLine();
            Console.Write("Digite a opção desejada: ");
            int optionCustomerMenu = int.Parse(Console.ReadLine()!);
            switch (optionCustomerMenu)
            {
                case 1:

                    MainTitle();
                    Console.WriteLine();
                    FormatTitles("Menu - Opções de Produto");
                    Console.WriteLine();
                    Console.WriteLine("1 - Cadastrar um novo produto\n2 - Remover produto\n3 - Alterar dados de um produto\n4 - ver catalogo");
                    Console.WriteLine("Digite a opção desejada: ");
                    int op = int.Parse(Console.ReadLine()!);
                    if (op == 1)
                    {
                        MainTitle();
                        Console.WriteLine();
                        FormatTitles("Cadastrando um novo Produto");
                        Console.WriteLine();
                        Random aleatorio = new Random();
                        int auxId = aleatorio.Next(100);

                        while (catalog.products.FirstOrDefault(x => x.ProductId == auxId) != null)
                        {
                            auxId = aleatorio.Next(100);
                        }

                        Console.Write("Digite o nome do produto: ");
                        string name = Console.ReadLine()!;
                        Console.Write("Digite a descrição do produto: ");
                        string description = Console.ReadLine()!;
                        Console.Write("Digite o preço do produto: ");
                        double price = double.Parse(Console.ReadLine()!, CultureInfo.InvariantCulture!);

                        catalog.AddProductToCatalog(new Product(auxId, name, description, price));
                        Console.WriteLine();
                        FormatTitles("Produto Adicionado com sucesso!!");
                        Console.WriteLine("aperte qualqer tecla pra voltar");
                        Console.ReadLine();

                        MainTitle();
                        AdministratorMenu(baseUsers, catalog, orderHistory, administrator);
                    }
                    else if (op == 2)
                    {

                        MainTitle();
                        Console.WriteLine();
                        FormatTitles("Removendo um produto");
                        Console.WriteLine();
                        Console.WriteLine(catalog);
                        Console.WriteLine();
                        FormatTitles("Removendo um produto!!");
                        Console.Write("Digite um Id do produto para ser removido: ");
                        int id = int.Parse(Console.ReadLine()!);

                        Product product = new Product();
                        product = catalog.products.FirstOrDefault(x => x.ProductId == id)!;

                        if (product != null)
                        {
                            catalog.RemoveProductToCatalog(product);
                            FormatTitles($"Produto de Id: {id}, removido com sucesso!");
                            Thread.Sleep(2000);
                            MainTitle();
                            AdministratorMenu(baseUsers, catalog, orderHistory, administrator);
                        }
                        else
                        {
                            Console.WriteLine("Id não localizado na Base!");
                            Thread.Sleep(2000);
                            MainTitle();
                            AdministratorMenu(baseUsers, catalog, orderHistory, administrator);
                        }
                    }
                    else if (op == 3)
                    {

                        MainTitle();
                        Console.WriteLine();
                        FormatTitles("Alterando um Produto");
                        Console.WriteLine();
                        Console.WriteLine(catalog);
                        Console.WriteLine();
                        FormatTitles("Alterando os dados de um produto!!");
                        Console.Write("Digite um Id do produto para ser alterado: ");
                        int id = int.Parse(Console.ReadLine()!);
                        Product product = new Product();
                        product = catalog.products.FirstOrDefault(x => x.ProductId == id)!;
                        if (product != null)
                        {
                            Console.WriteLine("Deseja alterar qual dado? \n1 - Nome\n2 - Descrição\n3 - Preço");
                            int option = int.Parse(Console.ReadLine()!);

                            if (option == 1)
                            {
                                Console.Write("Nome: ");
                                product.Name = Console.ReadLine();

                            }
                            if (option == 2)
                            {
                                Console.Write("Descrição: ");
                                product.Description = Console.ReadLine();

                            }
                            if (option == 3)
                            {
                                Console.Write("Price: ");
                                product.Price = double.Parse(Console.ReadLine()!, CultureInfo.InvariantCulture);

                            }
                            catalog.ChangeCatalogProduct(product);


                            Console.WriteLine("aperte qualqer tecla pra voltar");
                            Console.ReadLine();
                            MainTitle();
                            AdministratorMenu(baseUsers, catalog, orderHistory, administrator);
                        }
                        else
                        {
                            Console.WriteLine("Id não localizado na Base!");
                            Thread.Sleep(2000);
                            MainTitle();
                            AdministratorMenu(baseUsers, catalog, orderHistory, administrator);
                        }

                    }
                    if (op == 4)
                    {
                        MainTitle();
                        FormatTitles("Catalogo da loja");
                        Console.WriteLine(catalog);
                        Console.WriteLine("aperte qualqer tecla pra voltar");
                        Console.ReadLine();
                        MainTitle();
                        AdministratorMenu(baseUsers, catalog, orderHistory, administrator);
                    }
                    else
                    {

                        MainTitle();
                        AdministratorMenu(baseUsers, catalog, orderHistory, administrator);
                    }
                    break;
                case 2:

                    MainTitle();
                    FormatTitles("Menu - Opções de Relatório");
                    Console.WriteLine();
                    Reports(baseUsers, catalog, orderHistory, administrator);
                    break;
                case 3:
                    MainTitle();
                    FormatTitles("Lista de Usúarios Cadastrados: ");
                    Console.WriteLine(baseUsers);
                    Console.WriteLine("aperte qualqer tecla pra voltar");
                    Console.ReadLine();
                    MainTitle();
                    AdministratorMenu(baseUsers, catalog, orderHistory, administrator);
                    break;
                case 4:
                    MainTitle();
                    MainMenu(baseUsers, catalog, orderHistory);
                    break;
            }
        }
        public static void CustomerMenu(BaseUser baseUsers, Catalog catalog, OrderHistory orderHistory, Customer customer)
        {
            FormatTitles("Menu do Cliente");
            Console.WriteLine();
            Console.WriteLine("1 - Fazer Pedido\n2 - Ver Pedidos\n3 - Histórico de Compras\n4 - Logout\n");
            Console.WriteLine();
            Console.Write("Digite a opção desejada: ");
            int optionCustomerMenu = int.Parse(Console.ReadLine()!);
            switch (optionCustomerMenu)
            {
                case 1:


                    MainTitle();
                    RegisterOrder(baseUsers, catalog, orderHistory, customer);
                    break;
                case 2:

                    MainTitle();
                    Console.WriteLine();
                    FormatTitles("Dados do Seu Pedido");
                    OrdersInProgress(baseUsers, catalog, orderHistory, customer);
                    FinalizePayment(baseUsers, catalog, orderHistory, customer);
                    break;
                case 3:

                    MainTitle();
                    PurchaseHistoric(baseUsers, catalog, orderHistory, customer);
                    break;
                case 4:

                    MainTitle();
                    MainMenu(baseUsers, catalog, orderHistory);
                    break;
            }
        }
        public static void RegisterProductInCatalogManually(Catalog catalog)
        {
            Product p1 = new Product(1, "God Of War 1", "God 4 Para todos os amantes de Jogos Nordicos", 550.00, 1);
            Product p2 = new Product(2, "The Last of US", "Zumbis e Terror", 600.00, 1);
            Product p3 = new Product(3, "Resident Evill Village", "MEDO MEDO MEDO MEDO MEDO MEDO MEDO!!!", 700.00, 1);
            Product p4 = new Product(4, "Fifa 24", "Futebol de cria", 800.00, 1);
            catalog.AddProductToCatalog(p1);
            catalog.AddProductToCatalog(p2);
            catalog.AddProductToCatalog(p3);
            catalog.AddProductToCatalog(p4);
        }
        public static void RegisterOrder(BaseUser baseUsers, Catalog catalog, OrderHistory orderHistory, Customer customer)
        {
            FormatTitles("CATÁLOGO DA LOJA");
            Console.WriteLine(catalog);
            Console.WriteLine();
            char controle;
            Console.Write("Deseja selecionar um produto da lista? (s/n): ");
            Console.WriteLine();
            Console.Write("Selecione a opção desejada: ");
            controle = char.Parse(Console.ReadLine()!);
            if (controle == 'n')
            {
                MainTitle();
                CustomerMenu(baseUsers, catalog, orderHistory, customer);
            }
            else if (controle != 'n' && controle != 's')
            {
                Console.WriteLine();
                FormatTitles("Opção Invalida");
                Thread.Sleep(2000);
                MainTitle();
                CustomerMenu(baseUsers, catalog, orderHistory, customer);
            }
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
            order.Customer = customer;
            orderHistory.AddOrder(order);
            Thread.Sleep(4000);
            FormatTitles("Pedido Realizado com Sucesso");
            Console.WriteLine("Obrigado por comprar conosco!!!!! ");
            Thread.Sleep(3000);

            MainTitle();
            Console.WriteLine();
            CustomerMenu(baseUsers, catalog, orderHistory, customer);


        }
        public static void FinalizePayment(BaseUser baseUsers, Catalog catalog, OrderHistory orderHistory, Customer customer)
        {
            Console.WriteLine();
            FormatTitles("Deseja Finalizar o pedido? (s/n): ");
            Console.WriteLine("Digite a opção desejada: ");
            char op = char.Parse(Console.ReadLine()!);
            Console.WriteLine();
            switch (op)
            {
                case 's':
                    Console.WriteLine("Informe o ID do Pedido: ");
                    int id = int.Parse(Console.ReadLine()!);
                    Console.WriteLine();
                    FormatTitles("Aguarde um Instante, Pagamento esta sendo processado!!!");

                    StatusOrder status = new StatusOrder();
                    status = Enum.Parse<StatusOrder>("Delivered");

                    foreach (Order orderHistoryList in orderHistory.orders)
                    {
                        if (orderHistoryList.OrderId == id)
                        {
                            orderHistoryList.Status = status;
                            break;
                        }
                    }
                    Thread.Sleep(3000);
                    FormatTitles("Pagamento Realizado com sucesso!!!");
                    Thread.Sleep(3000);

                    MainTitle();
                    CustomerMenu(baseUsers, catalog, orderHistory, customer);

                    break;
                case 'n':

                    MainTitle();
                    CustomerMenu(baseUsers, catalog, orderHistory, customer);
                    break;

            }

        }
        public static void PurchaseHistoric(BaseUser baseUsers, Catalog catalog, OrderHistory orderHistory, Customer customer)
        {
            StatusOrder status = new StatusOrder();
            status = Enum.Parse<StatusOrder>("Delivered");
            Console.WriteLine("==========================================================================");
            if (orderHistory.orders.FirstOrDefault(x => x.Customer?.UserId == customer.UserId) != null &&
                orderHistory.orders.FirstOrDefault(x => x.Status == status) != null)
            {
                foreach (Order order in orderHistory.orders)
                {
                    if (order.Status == status)
                    {
                        if (order.Status == status && order.Customer?.UserId == customer.UserId)
                        {
                            double valorTotalDoPedido = 0.00;
                            Console.Write("Id do Pedido: " + order.OrderId + "\n");
                            Console.Write("Data do pedido: " + order.Moment + "\n");
                            Console.Write("Status: " + order.Status + "\n");
                            Console.Write("Cliente: " + order.Customer.Name + "\n");
                            FormatTitles("*** Produtos Adqueridos nesse pedido: ****");
                            foreach (Product product in order.Products)
                            {
                                Console.Write("Produto: " + product.Name + ", R$ " + product.Price.ToString("F2", CultureInfo.InvariantCulture) + "\n");
                                valorTotalDoPedido += product.Price;
                            }
                            Console.WriteLine();
                            FormatTitles("***Valor total do Pedido: " + "R$" + valorTotalDoPedido.ToString("F2", CultureInfo.InvariantCulture) + "***");
                            Console.WriteLine("===================================================================================");
                        }

                    }
                }

                Console.WriteLine();
                Console.Write("Digite qualquer tecla para voltar ao menu principal: ");
                Console.ReadLine();

                MainTitle();
                CustomerMenu(baseUsers, catalog, orderHistory, customer);
            }

            else
            {
                FormatTitles("Usúario ainda não Finalizou uma compra! Veja os pedidos no menu Principal");
                Thread.Sleep(3000);

                MainTitle();
                CustomerMenu(baseUsers, catalog, orderHistory, customer);

            }
        }
        public static void OrdersInProgress(BaseUser baseUsers, Catalog catalog, OrderHistory orderHistory, Customer customer)
        {
            StatusOrder status = new StatusOrder();
            status = Enum.Parse<StatusOrder>("Processing");
            Console.WriteLine("==========================================================================");
            if (orderHistory.orders.FirstOrDefault(x => x.Customer?.UserId == customer.UserId) != null &&
                orderHistory.orders.FirstOrDefault(x => x.Status == status) != null)
            {
                foreach (Order order in orderHistory.orders)
                {
                    if (order.Status == status && order.Customer?.UserId == customer.UserId)
                    {
                        double valorTotalDoPedido = 0.00;
                        Console.Write("Id do Pedido: " + order.OrderId + "\n");
                        Console.Write("Data do pedido: " + order.Moment + "\n");
                        Console.Write("Status: " + order.Status + "\n");
                        Console.Write("Cliente: " + order.Customer.Name + "\n");
                        FormatTitles("*** Produtos Adqueridos nesse pedido: ****");
                        foreach (Product product in order.Products)
                        {
                            Console.Write("Produto: " + product.Name + ", R$ " + product.Price.ToString("F2", CultureInfo.InvariantCulture) + "\n");
                            valorTotalDoPedido += product.Price;
                        }
                        Console.WriteLine();
                        FormatTitles("***Valor total do Pedido: " + "R$" + valorTotalDoPedido.ToString("F2", CultureInfo.InvariantCulture) + "***");
                        Console.WriteLine("======================================================================================");
                    }

                }

            }
            else
            {
                FormatTitles("Usúario ainda não possiu pedido! Para fazer um pedido acesse o catalogo da loja");
                Thread.Sleep(3000);

                MainTitle();
                CustomerMenu(baseUsers, catalog, orderHistory, customer);
            }
        }
        public static void Reports(BaseUser baseUsers, Catalog catalog, OrderHistory orderHistory, Administrator administrator)
        {
            Console.WriteLine("1 - Total de Vendas por Clientes\n2 - Total de vendas por Produtos\n3 - Total de Pedidos não finalizados por cliente");
            int op = int.Parse(Console.ReadLine()!);
            Console.WriteLine();
            double totalVendido = 0.00;
            if (op == 1)
            {
                MainTitle();
                Console.WriteLine();
                StatusOrder status = Enum.Parse<StatusOrder>("Delivered");
                FormatTitles("Relatório de vendas por cliente: ");
                Console.WriteLine();
                Console.WriteLine("========================================================================");

                foreach (User user in baseUsers.users)
                {
                    foreach (Order order in orderHistory.orders)
                    {
                        if (order.Customer!.UserId == user.UserId && order.Status == status)
                        {

                            foreach (Product product in order.Products)
                            {
                                totalVendido += product.Price;
                            }
                            if (totalVendido != 0)
                            {
                                Console.WriteLine("Nome do Cliente: " + user.Name + ", Total vendido: R$ " + totalVendido.ToString("F2", CultureInfo.InvariantCulture));
                                Console.WriteLine("========================================================================");
                            }
                        }
                        totalVendido = 0;
                    }

                }
                Console.WriteLine();
                Console.WriteLine("aperte qualqer tecla pra voltar");
                Console.ReadLine();
                MainTitle();
                AdministratorMenu(baseUsers, catalog, orderHistory, administrator);

            }
            else if (op == 2)
            {
                MainTitle();
                Console.WriteLine();
                FormatTitles("**RELATÓRIO DE VENDAS POR PRODUTO**");
                StatusOrder status = Enum.Parse<StatusOrder>("Delivered");
                int totalPerProduct = 0;
                double valueTotalSaledPerProduct = 0.00;

                Console.WriteLine("=====================================================");
                foreach (Product product in catalog.products)
                {
                    foreach (Order order in orderHistory.orders)
                    {
                        foreach (Product auxProduct in order.Products)
                        {
                            if (auxProduct.ProductId == product.ProductId && order.Status == status)
                            {
                                totalPerProduct += product.Quantity;
                                valueTotalSaledPerProduct = product.Price * totalPerProduct;
                            }
                        }
                    }
                    if (totalPerProduct != 0)
                    {
                        Console.WriteLine("Nome do Produto: "
                                            + product.Name
                                            + "\nQuantidade Vendido: "
                                            + totalPerProduct + "\nValor Total Vendido: R$ "
                                            + valueTotalSaledPerProduct.ToString("F2", CultureInfo.InvariantCulture));
                        Console.WriteLine("=====================================================");
                        totalPerProduct = 0;
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Digite qualquer tecla para voltar ao menu principal: ");
                Console.ReadLine();
                MainTitle();
                AdministratorMenu(baseUsers, catalog, orderHistory, administrator);
            }
            else if (op == 3)
            {
                MainTitle();
                Console.WriteLine();
                FormatTitles("Relatório de Pedidos não finalizados por cliente");
                StatusOrder status = Enum.Parse<StatusOrder>("Delivered");
                int totalUnfinishedOrders = 0;
                Console.WriteLine("========================================================================");

                foreach (User user in baseUsers.users)
                {
                    foreach (Order order in orderHistory.orders)
                    {
                        if (order.Customer!.UserId == user.UserId && order.Status != status)
                        { 
                            totalUnfinishedOrders++;
                        }
                    }
                    if (totalUnfinishedOrders != 0)
                    {
                        Console.WriteLine("Nome do Cliente: " + user.Name + "\n" +
                        "Quantidade: " + totalUnfinishedOrders + " unidades");
                        Console.WriteLine("========================================================================");
                    }
                    totalUnfinishedOrders = 0;
                }
                Console.WriteLine();
                Console.WriteLine("Aperte qualquer tecla para voltar: ");
                Console.ReadLine();
                MainTitle();
                AdministratorMenu(baseUsers, catalog, orderHistory, administrator);

            }
        }
    }
}



