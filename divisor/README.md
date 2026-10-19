# Divisor de ROMs

GitHub no acepta archivos de más de 100 MB. Esta herramienta corta las ROMs
grandes en partes de 90 MB que después `index.html` vuelve a unir al jugar.

1. Copiá tus ROMs (`.nds`, `.n64`, `.z64`, `.v64`, `.sfc`, `.smc`, ...) a esta carpeta.
2. Abrí `divisor.exe`.
3. Copiá todo lo que aparece en `divididos/` a `roms/<consola>/`
   (por ejemplo `roms/nds/`).
4. Commit y push. El workflow detecta las partes (`Juego.nds.001`,
   `Juego.nds.002`, ...) y las muestra en el menú como un solo juego: **Juego**.

Las ROMs que ya pesan menos de 100 MB se copian enteras, sin dividir.

Las ROMs y la carpeta `divididos/` están en `.gitignore`, así que no se suben
por error desde acá.

Si modificás `Divisor.cs`, ejecutá `compilar.bat` para regenerar el `.exe`.
