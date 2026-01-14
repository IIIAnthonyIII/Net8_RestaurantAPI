Clean Arquitecture

*References
Aplication 	=> Domain
Infrastructure  => Application
Infrastructure  => Domain
API 		=> Application
API		=> Infrastructure

*Prioridad
API => Establecer como proyecto de inicio

*Paquetes NuGet CMD
add-migration Init 	    => Crear archivo para migración
update-database		    => Correr migración
add-migration nameMigration => Agregar un cambio en las tablas

Se añadió Integración continua (Continuos Integration) Github
