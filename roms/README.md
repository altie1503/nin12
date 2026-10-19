# Cómo agregar juegos

Poné tus archivos de ROM en la carpeta de la consola correspondiente:

- `roms/n64/` → archivos `.z64`, `.n64` o `.v64` (Nintendo 64)
- `roms/snes/` → archivos `.sfc`, `.smc`, `.swc`, `.fig`, `.bs` o `.st` (Super Nintendo)
- `roms/nds/` → archivos `.nds` (Nintendo DS)

No hace falta editar ningún código ni archivo de índice: el workflow de
GitHub Actions (`.github/workflows/deploy-pages.yml`) escanea estas carpetas
en cada push a `main` y genera automáticamente `games.json`, que es lo que usa
`index.html` para armar el menú de selección de juegos.

El nombre del archivo (sin la extensión) es exactamente el nombre que se
muestra en el menú, así que nombralo como quieras verlo (por ejemplo
`Pokemon Platino.nds`).

## Límite de tamaño

GitHub rechaza archivos de más de 100 MB. Para ROMs más grandes (muchas de DS
pesan 128–512 MB) usá `divisor/divisor.exe`: las corta en partes
`Juego.nds.001`, `Juego.nds.002`, ... que se copian tal cual a la carpeta de la
consola. El menú las muestra como un solo juego y el navegador las vuelve a unir
al jugar. Más detalles en `divisor/README.md`.

Ojo: todo el sitio de GitHub Pages tiene un límite recomendado de 1 GB en
total (el emulador ya ocupa unos 300 MB).
