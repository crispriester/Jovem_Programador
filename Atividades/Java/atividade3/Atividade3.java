package Java;
/*maça: 1,50 kg
ovos: 14 duzia
arroz: 3,50 kg

quantos kg de maça?
quantas duzias?
quantos kg de arroz?

calcular o total da venda?
calcular o valor do imposto a pagar? (10%)
calcular o valor do lucro? (90%)
*/
public class Vendas {

    public static void main(String[] args) {

        double maca, ovos, arroz;

        Scanner leitor = new Scanner(System.in);

        System.out.print("Digite quantos kg de maçã queres comprar: ");
        qntd_maca = leitor.nextFloat();

        System.out.print("Digite quantas dúzias queres comprar: ");
        qntd_ovos = leitor.nextFloat();

        System.out.print("Digite quantos kg de arroz queres comprar: ");
        qntd_arroz = leitor.nextFloat();

        leitor.close();

        final float preco_maca = 1.5;
        final float preco_ovos = 14.0;
        final float preco_arroz = 3.5;

        total_venda = (qntd_maca * preco_maca) +  (qntd_ovos * preco_ovos) + (qntd_arroz * preco_arroz);
        total_imposto = total_venda * 0.1f;
        total_lucro = total_venda - total_imposto;

        System.out.println("\nLucro: " + total_lucro + "\nImposto: " + total_imposto + "\nTotal: " + total_venda);
    }
}