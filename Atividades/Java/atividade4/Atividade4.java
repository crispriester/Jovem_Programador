package MediaAritmetica.src;
/*
Leia 3 notas de um aluno e faça uma média ponderada. Cada nota tem um peso diferente.
*/
public class MediaPonderada {

    public static void main(String[] args) {
        
        Scanner leitor = new Scanner(System.in);

        System.out.print("Digite a primeira nota: ")
        float nota1 = leitor.nextFloat();

        System.out.print("Digite a segunda nota: ")
        float nota2 = leitor.nextFloat();
        
        System.out.print("Digite a terceira nota: ")
        float nota3 = leitor.nextFloat();

        leitor.close()

        byte peso1 = 3;
        byte peso2 = 2;
        byte peso3 = 5;

        float media_ponderada = 0;

        media_ponderada = ((nota1 * peso1) + (nota2 * peso2) + (nota3 * peso3)) / (peso1 + peso2 + peso3);

        System.out.println("Média Ponderada: " + media_ponderada);
    }
}