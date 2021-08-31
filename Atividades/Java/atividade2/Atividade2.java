package Java;
import java.util.Scanner;
/*
Leia dois números e faça a soma deles.
*/
public class Atividade2 {

    public static void main(String[] args) {

        Scanner leitor = new Scanner(System.in);

        System.out.print ("digite um numero: ");
        int numero1 = leitor.nextInt();

        System.out.print ("digite um numero");
        int numero2 = leitor.nextInt();

        int resultado = numero1 + numero2;
        
        leitor.close();
        
        System.out.print ("resultado: " + resultado);
    }
}