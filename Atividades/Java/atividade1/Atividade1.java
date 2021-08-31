package Java;
/* 
Leia o raio de um círculo e calcule sua área e seu perímetro.
*/
public class Atividade1 {

    public static void main(String[] args) {

        float raio, area, perimetro;

        Scanner leitor = new Scanner(System.in);

        System.out.print("Digite o raio: ");
        raio = leitor.nextFloat();

        leitor.close();

        area = 3.14 * (raio ^ 2);
        perimetro = 2 * 3.14 * raio;

        System.out.println("Área: " + area);
        System.out.println("Perímetro: " + perimetro);

    }
}