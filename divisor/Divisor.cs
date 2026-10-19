// Divisor de ROMs: corta cada ROM de esta carpeta en partes de menos de 100 MB
// (límite de GitHub) y las deja en la subcarpeta "divididos".
// Las partes se llaman "Juego.nds.001", "Juego.nds.002", ... y el index.html
// las vuelve a unir al momento de jugar.
//
// Compilar: ejecutar compilar.bat (usa el csc.exe que ya trae Windows).

using System;
using System.IO;
using System.Text;

class Divisor
{
    // 90 MiB: deja margen bajo el límite de 100 MB de GitHub.
    const long TAMANO_PARTE = 90L * 1024 * 1024;

    static readonly string[] EXTENSIONES = {
        ".nds", ".n64", ".z64", ".v64", ".sfc", ".smc", ".swc", ".fig", ".bs", ".st"
    };

    static int Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        string carpeta = AppDomain.CurrentDomain.BaseDirectory;
        string salida = Path.Combine(carpeta, "divididos");
        int procesados = 0;

        Console.WriteLine("=== Divisor de ROMs ===");
        Console.WriteLine("Buscando ROMs en: " + carpeta);
        Console.WriteLine();

        try
        {
            foreach (string rom in Directory.GetFiles(carpeta))
            {
                string ext = Path.GetExtension(rom).ToLowerInvariant();
                if (Array.IndexOf(EXTENSIONES, ext) < 0) continue;

                Directory.CreateDirectory(salida);
                Procesar(rom, salida);
                procesados++;
            }

            if (procesados == 0)
            {
                Console.WriteLine("No se encontraron ROMs. Copiá tus archivos (" +
                    string.Join(", ", EXTENSIONES) + ") junto a este .exe y volvé a abrirlo.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Listo: " + procesados + " ROM(s). Copiá el contenido de");
                Console.WriteLine("  " + salida);
                Console.WriteLine("a la carpeta roms/<consola>/ del proyecto (por ejemplo roms/nds/).");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: " + ex.Message);
            Esperar();
            return 1;
        }

        Esperar();
        return 0;
    }

    static void Procesar(string rom, string salida)
    {
        string nombre = Path.GetFileName(rom);
        long tamano = new FileInfo(rom).Length;

        // Borrar partes viejas de este mismo juego para no mezclar versiones.
        foreach (string viejo in Directory.GetFiles(salida, nombre + ".*"))
        {
            File.Delete(viejo);
        }
        string entero = Path.Combine(salida, nombre);
        if (File.Exists(entero)) File.Delete(entero);

        if (tamano <= TAMANO_PARTE)
        {
            File.Copy(rom, entero);
            Console.WriteLine(nombre + " (" + Mb(tamano) + ") ya pesa menos de 100 MB: copiado sin dividir.");
            return;
        }

        int partes = (int)((tamano + TAMANO_PARTE - 1) / TAMANO_PARTE);
        Console.WriteLine(nombre + " (" + Mb(tamano) + ") -> " + partes + " partes");

        byte[] buffer = new byte[1024 * 1024];
        using (FileStream entrada = File.OpenRead(rom))
        {
            for (int i = 1; i <= partes; i++)
            {
                string parte = Path.Combine(salida, nombre + "." + i.ToString("000"));
                long restante = Math.Min(TAMANO_PARTE, tamano - entrada.Position);
                using (FileStream destino = File.Create(parte))
                {
                    while (restante > 0)
                    {
                        int leidos = entrada.Read(buffer, 0, (int)Math.Min(buffer.Length, restante));
                        if (leidos <= 0) throw new IOException("Se terminó el archivo antes de lo esperado.");
                        destino.Write(buffer, 0, leidos);
                        restante -= leidos;
                    }
                }
                Console.WriteLine("   " + Path.GetFileName(parte));
            }
        }
    }

    static string Mb(long bytes)
    {
        return (bytes / (1024.0 * 1024.0)).ToString("0.0") + " MB";
    }

    static void Esperar()
    {
        Console.WriteLine();
        Console.WriteLine("Presioná Enter para cerrar...");
        Console.ReadLine();
    }
}
