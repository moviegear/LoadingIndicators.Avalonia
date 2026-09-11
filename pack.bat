@echo off

set /p VERSION=Enter package version: 

REM Strip leading "v" if entered, e.g. v1.2.3 -> 1.2.3
if /i "%VERSION:~0,1%"=="v" set "VERSION=%VERSION:~1%"

dotnet restore LoadingIndicators.Avalonia/LoadingIndicators.Avalonia.csproj
dotnet build LoadingIndicators.Avalonia/LoadingIndicators.Avalonia.csproj -c Release --no-restore
dotnet pack LoadingIndicators.Avalonia/LoadingIndicators.Avalonia.csproj -c Release --no-build -o ./artifacts -p:Version=%VERSION%

REM Generate Preview
REM GIF:
REM ffmpeg -framerate 30 -i frame_%04d.png -filter_complex "color=c=#2b2b2b:s=1280x720:r=24[bg];[0:v]scale=1280:720:flags=lanczos[fg];[bg][fg]overlay=shortest=1:format=auto,fps=24,split[s0][s1];[s0]palettegen=stats_mode=diff:max_colors=128[p];[s1][p]paletteuse=dither=none:diff_mode=rectangle" -loop 0 output.gif

REM WEBP:
REM ffmpeg -framerate 30 -i frame_%04d.png -filter_complex "color=c=#2b2b2b:s=1280x720:r=24[bg];[0:v]scale=1280:720:flags=lanczos[fg];[bg][fg]overlay=shortest=1:format=auto,fps=24" -c:v libwebp -lossless 0 -quality 80 -compression_level 6 -loop 0 -an output.webp
