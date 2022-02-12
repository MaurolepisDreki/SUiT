# GNU/Make wrapper for dotnet

.PHONY: all run build restore clean purge

all: run

run:
	dotnet run --project SUiT/SUiT.csproj

build:
	dotnet build

restore:
	dotnet restore

clean:
	dotnet clean

purge:
	git clean -xfd

