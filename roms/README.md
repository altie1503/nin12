# Cómo agregar juegos

Poné tus archivos de ROM en la carpeta de la consola correspondiente:

- `roms/n64/` → archivos `.z64`, `.n64` o `.v64` (Nintendo 64)
- `roms/snes/` → archivos `.sfc`, `.smc`, `.swc`, `.fig`, `.bs` o `.st` (Super Nintendo)

No hace falta editar ningún código ni archivo de índice: el workflow de
GitHub Actions (`.github/workflows/deploy-pages.yml`) escanea estas dos
carpetas en cada despliegue y genera automáticamente `games.json`, que es lo
que usa `index.html` para armar el menú de selección de juegos.

El nombre del archivo (sin la extensión) es el nombre que se muestra en el
menú, así que conviene nombrarlo como el juego (por ejemplo
`Super Mario 64 (USA).z64`).

Estos archivos pesan varios MB/GB y ya están configurados en `.gitattributes`
para subirse vía Git LFS — simplemente hacé `git add` normal, Git se encarga
del resto.
