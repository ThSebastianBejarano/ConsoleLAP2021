using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ConsoleLAP2021
{
    class Program
    {
        static void Main(string[] args)
        {
            Tareas tarea = new Tareas();

            //Threads 1, 2, 3, 4.
            Thread thread1 = new Thread(tarea.tarea1);
            Thread thread2 = new Thread(tarea.tarea2);
            Thread thread3 = new Thread(tarea.tarea3);
            Thread thread4 = new Thread(tarea.tarea4);

            //Inicio Threads en paralelo
            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();

            Console.ReadKey();
        }
    }

    class Tareas
    {

        private List<string> lineas = new List<string>();
        private Object bloqueo = new object();

        public void tarea1() 
        {
            char[] cadena = { ' ', '.', ',', ';', ':','-'};
            char letra = 'n';
            escribir_texto_salida(1, leer_texto(cadena, letra, false), "Palabras que terminan en 'n' en el Texto.");
        }
        public void tarea2()
        {
            char[] cadena = {'.'};
            char letra = ' ';
            escribir_texto_salida(2, leer_texto(cadena, letra, false), "Oraciones en el Texto.");
        }
        public void tarea3() 
        {
            char[] cadena = { '\r' };
            char letra = ' ';
            escribir_texto_salida(3, leer_texto(cadena, letra, false), "Parrafos en el Texto.");
        }
        public void tarea4() 
        {
            char[] cadena = {' ', '.', ',', ';', ':', '-', 'n', 'N'};
            char letra = ' ';
            escribir_texto_salida(4, leer_texto(cadena, letra, true), "Caracteres en el Texto.");
        }

        //Funcion para leer el archivo de entrada.
        public int leer_texto(char[] cadena, char letra, bool cont)
        {
            int numero_final = 0;
            try
            {     
                using (StreamReader leer = new StreamReader("./Test.txt"))
                {
                    string[] palabras = leer.ReadToEnd().Split(cadena);
                    for (int i = 0; i < palabras.Length; i++)
                    {
                        if (letra != ' ')
                        {
                            if (palabras[i].EndsWith(letra)) numero_final++;
                        }
                        else if (cont == true) numero_final += palabras[i].Length;
                        else numero_final++;

                    }
                }
            }
            catch (FileNotFoundException fe)
            {
                Console.Write("No se encontro el Archivo de Entrada... " + fe);
            }
        return numero_final;
        }

        //Funcion para escrbir y leer el archivo de salida.
        public void escribir_texto_salida(int numero_tarea, int numero, string frase)
        {
            lock(bloqueo)
            {
                using (StreamWriter escribir = new StreamWriter("./TestSalida.txt"))
                {
                    lineas.Add($"Tarea #{numero_tarea}: Se Encontraron {numero} {frase}");
                    for (int i = 0; i < lineas.Count; i++) escribir.WriteLine(lineas[i]);
                }
                using (StreamReader leer = new StreamReader("./TestSalida.txt"))
                {
                    String texto = leer.ReadToEnd();
                    //Console.Clear(); //Esta siguiente Linea realiza la limpieza de toda la consola, quedando solo el ultimo Thread
                    Console.Write(texto);
                }
            }
        }

    }
}
