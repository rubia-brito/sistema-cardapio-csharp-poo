using System.Collections.Generic;

public class Pedido
{
    public List<Item> Itens { get; set; } = new List<Item>();
    private int contadorId = 1;

    public void AdicionarItem(Item item)
    {
        item.Id = contadorId++;
        Itens.Add(item);
    }

    public void RemoverItem(int id)
    {
        Itens.RemoveAll(i => i.Id == id);
    }

    public double Total()
    {
        double total = 0;

        foreach (var item in Itens)
        {
            total += item.Subtotal();
        }

        return total;
    }
}