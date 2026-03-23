using System;

class Program
{
    static Pedido pedido = new Pedido();

    static void Main()
    {
        int opcao;

        do
        {
            Console.Clear();
            Console.WriteLine("**************************");
            Console.WriteLine("===== MENU PRINCIPAL =====");
            Console.WriteLine("      1 - Lanches         ");
            Console.WriteLine("      2 - Acompanhamentos ");
            Console.WriteLine("      3 - Bebidas         ");
            Console.WriteLine("      0 - Finalizar Pedido");
            Console.WriteLine("**************************");

            opcao = LerNumero();

            switch (opcao)
            {
                case 1: MenuLanches(); break;
                case 2: MenuAcompanhamentos(); break;
                case 3: MenuBebidas(); break;
            }

        } while (opcao != 0);

        Console.WriteLine("\nDeseja ajustar o pedido antes de finalizar?");
        Console.WriteLine("1 - Sim");
        Console.WriteLine("0 - Não");

         int ajuste = LerNumero();

        if (ajuste == 1)
           {
             AjustarPedido();
           }

            MostrarResumoFinal();
    }

    static int LerNumero()
    {
        int valor;
        while (!int.TryParse(Console.ReadLine(), out valor))
        {
            Console.Write("Digite um número válido: ");
        }
        return valor;
    }

    static int LerQuantidade()
    {
        Console.Write("Quantidade: ");
        int qtd;
        while (!int.TryParse(Console.ReadLine(), out qtd) || qtd <= 0)
        {
            Console.Write("Quantidade inválida: ");
        }
        return qtd;
    }

    static void AdicionarItem(string nome, double preco, int qtd, string categoria)
    {
        pedido.AdicionarItem(new Item
        {
            Nome = nome,
            Preco = preco,
            Quantidade = qtd,
            Categoria = categoria
        });

        Console.WriteLine("Item adicionado com sucesso!");
        Console.ReadKey();
    }

    static void MenuLanches()
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("***************************");
            Console.WriteLine("==========LANCHES==========");
            Console.WriteLine("    1 - Misto Quente (6.50)");
            Console.WriteLine("    2 - Hambúrguer (10.25) ");
            Console.WriteLine("    3 - X-Salada (12.00)   ");
            Console.WriteLine("    0 - Voltar             ");
            Console.WriteLine("***************************");
            opcao = LerNumero();

            if (opcao == 0)
            {
                FluxoSaidaCategoria("Lanche");
                break;
            }

            int qtd = LerQuantidade();

            switch (opcao)
            {
                case 1: AdicionarItem("Misto Quente", 6.50, qtd, "Lanche"); break;
                case 2: AdicionarItem("Hambúrguer", 10.25, qtd, "Lanche"); break;
                case 3: AdicionarItem("X-Salada", 12.00, qtd, "Lanche"); break;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }

        } while (true);
    }

    static void MenuAcompanhamentos()
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("***************************");
            Console.WriteLine("======ACOMPANHAMENTOS======");
            Console.WriteLine("   1 - Batata Frita (10.00)");
            Console.WriteLine("   2 - Nuggets (14.00)     ");
            Console.WriteLine("   0 - Voltar              ");
            Console.WriteLine("***************************");
            
            opcao = LerNumero();

            if (opcao == 0)
            {
                FluxoSaidaCategoria("Acompanhamento");
                break;
            }

            int qtd = LerQuantidade();

            switch (opcao)
            {
                case 1: AdicionarItem("Batata Frita", 10.00, qtd, "Acompanhamento"); break;
                case 2: AdicionarItem("Nuggets", 14.00, qtd, "Acompanhamento"); break;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }

        } while (true);
    }

    static void MenuBebidas()
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("***************************");
            Console.WriteLine("==========BEBIDAS==========");
            Console.WriteLine("  1 - Refrigerante (6.00)  ");
            Console.WriteLine("  2 - Suco (6.00)          ");
            Console.WriteLine("  0 - Voltar               ");
            Console.WriteLine("***************************");

            opcao = LerNumero();

            if (opcao == 0)
            {
                FluxoSaidaCategoria("Bebida");
                break;
            }

            int qtd = LerQuantidade();

            switch (opcao)
            {
                case 1: AdicionarItem("Refrigerante", 6.00, qtd, "Bebida"); break;
                case 2: AdicionarItem("Suco", 6.00, qtd, "Bebida"); break;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }

        } while (true);
    }

    static void FluxoSaidaCategoria(string categoria)
    {
        Console.Clear();
        MostrarResumoCategoria(categoria);

        Console.WriteLine("\nDeseja remover algum item?");
        Console.WriteLine("1 - Sim");
        Console.WriteLine("0 - Não");

        int escolha = LerNumero();

        if (escolha == 1)
        {
            RemoverItemCategoria(categoria);
            Console.Clear();
            MostrarResumoCategoria(categoria);
            Console.WriteLine("\nPressione uma tecla...");
            Console.ReadKey();
        }
    }

    static void MostrarResumoCategoria(string categoria)
    {
        double subtotal = 0;

        Console.WriteLine($"\n--- Itens de {categoria} ---\n");

        foreach (var item in pedido.Itens)
        {
            if (item.Categoria == categoria)
            {
                Console.WriteLine($"ID {item.Id} - {item.Quantidade}x {item.Nome} - R$ {item.Subtotal():F2}");
                subtotal += item.Subtotal();
            }
        }

        Console.WriteLine($"\nSubtotal {categoria}: R$ {subtotal:F2}");
    }

    static void RemoverItemCategoria(string categoria)
    {
        Console.Write("\nDigite o ID do item para remover (0 para cancelar): ");
        int id = LerNumero();

        if (id == 0) return;

        var item = pedido.Itens.Find(i => i.Id == id && i.Categoria == categoria);

        if (item != null)
        {
            Console.Write($"Quantas unidades deseja remover? (Máx: {item.Quantidade}): ");
            int qtdRemover = LerNumero();

            if (qtdRemover <= 0)
            {
                Console.WriteLine("Quantidade inválida!");
            }
            else if (qtdRemover >= item.Quantidade)
            {
                pedido.RemoverItem(id);
                Console.WriteLine("Item removido completamente!");
            }
            else
            {
                item.Quantidade -= qtdRemover;
                Console.WriteLine("Quantidade atualizada!");
            }
        }
        else
        {
            Console.WriteLine("Item não encontrado!");
        }

        Console.ReadKey();
    }

    static void MostrarResumoFinal()
    {
        Console.Clear();

        Console.WriteLine("===== RESUMO FINAL =====\n");

        foreach (var item in pedido.Itens)
        {
            Console.WriteLine($"{item.Quantidade}x {item.Nome} - R$ {item.Subtotal():F2}");
        }

        Console.WriteLine("\n------------------------");
        Console.WriteLine($"TOTAL: R$ {pedido.Total():F2}");
    }
 
    static void AjustarPedido()
  {
    while (true)
    {
        Console.Clear();

        if (pedido.Itens.Count == 0)
        {
            Console.WriteLine("Nenhum item no pedido.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("===== AJUSTAR PEDIDO =====\n");

        foreach (var item in pedido.Itens)
        {
            Console.WriteLine($"ID {item.Id} - {item.Quantidade}x {item.Nome} - R$ {item.Subtotal():F2}");
        }

        Console.WriteLine("\nDigite o ID para remover/editar");
        Console.WriteLine("0 - Voltar");

        int id = LerNumero();

        if (id == 0) break;

        var itemSelecionado = pedido.Itens.Find(i => i.Id == id);

        if (itemSelecionado != null)
        {
            Console.Write($"Quantas unidades deseja remover? (Máx: {itemSelecionado.Quantidade}): ");
            int qtd = LerNumero();

            if (qtd >= itemSelecionado.Quantidade)
            {
                pedido.RemoverItem(id);
                Console.WriteLine("Item removido!");
            }
            else if (qtd > 0)
            {
                itemSelecionado.Quantidade -= qtd;
                Console.WriteLine("Quantidade atualizada!");
            }
            else
            {
                Console.WriteLine("Quantidade inválida!");
            }
        }
        else
        {
            Console.WriteLine("Item não encontrado!");
        }

        Console.WriteLine("\nDeseja continuar ajustando?");
        Console.WriteLine("1 - Sim");
        Console.WriteLine("0 - Não");

        int continuar = LerNumero();

        if (continuar == 0) break;
    }
  }


}
