using System;
using System.Numerics;

class Program 
{
    static void Main() 
    {

        string[] q = {"q0", "q1", "q2", "q3", "q4"}; // Conjunto finito de estados

        int[] alfabeto = {0, 1}; // Alfabeto de simbolos de entrada

        string q0 = q[0]; // Estado inicial
        string[] f = {q[2], q[4]}; // Conjunto de estados finales

        int filas = q.Length;
        int columnas = alfabeto.Length;

        HashSet<string>[,] tran = new HashSet<string>[filas, columnas]; // Tabla de transicion de estados

        for (int i = 0; i < filas; i++)
            for (int j = 0; j < columnas; j++)
                tran[i, j] = new HashSet<string>();  // Se inicializa cada elemento del arreglo

        // Llenado de la tabla de transicion de estados
//     |-----------------------|------------------------|
//     |        0              |            1           |
//     |-----------------------|------------------------|
        tran[0, 0].Add(q[0]);     tran[0, 1].Add(q[0]);
        tran[0, 0].Add(q[3]);     tran[0, 1].Add(q[1]);
//     |-----------------------|------------------------|
        /*tran[1, 0] es vacio*/   tran[1, 1].Add(q[2]);
//     |-----------------------|------------------------|
        tran[2, 0].Add(q[2]);     tran[2, 1].Add(q[2]);
//     |-----------------------|------------------------|
        tran[3, 0].Add(q[4]);    /*tran[3, 1] es vacio*/
//     |-----------------------|------------------------|
        tran[4, 0].Add(q[4]);     tran[4, 1].Add(q[4]);
//     |-----------------------|------------------------|

        Console.Write("Ingrese la cadena a evaluar (Solo 0 y 1): ");
        string w = Console.ReadLine();

        Console.WriteLine("Q = {" + string.Join(", ", q) + "}");
        Console.WriteLine("Σ = {" + string.Join(", ", alfabeto) + "}");
        Console.WriteLine("q0 = " + q0);
        Console.WriteLine("F = {" + string.Join(", ", f) + "}");

        Console.WriteLine("Matriz de transicion de estados");
        for (int i = 0; i < filas; i++)
            Console.WriteLine("{" + string.Join(", ", tran[i, 0]) + "} | {" + string.Join(", ", tran[i, 1]) + "}");

        string[] estados = {q0}; // Reemplaza el valor de q en q0 por una cadena vacia, dejando solo el valor numero para realizar el parseo
        HashSet<string> conjunto = new HashSet<string>();

        for (int i = 0; i < w.Length; i++)
        {
            conjunto = new HashSet<string>();
            int simbolo = int.Parse(w[i].ToString());

            for (int j = 0; j < estados.Length; j++)
            {
                int estado = int.Parse(estados[j].Replace("q", ""));

                if (tran[estado, simbolo].Count != 0)
                    conjunto.UnionWith(tran[estado, simbolo]);
            }

            Console.WriteLine("δ({" + string.Join(", ", estados) + "}, " + simbolo + ") = " + 
            "{" + string.Join(", ", conjunto) + "}");

            estados = conjunto.ToArray();

        }

        string resultado = "";

        foreach (string item in f)
            if (estados.Contains(item))
                resultado += item + ", ";

        if (!resultado.Equals(""))
        {
            resultado = resultado.Substring(0, resultado.Length - 2);
            resultado += " ∈ F ∴ la cadena es aceptada";
        } else {
            resultado = "{" + string.Join(", ", estados) + "}" + " ∉ F ∴ la cadena no es aceptada";
        }

        Console.WriteLine(resultado);

    }

}