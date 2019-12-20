# Colombages

Pour importer le projet, il suffit d'importer le package `colombage.unitypackage` dans un projet vide existant.

L'objet HouseSpawner possède un bouton `Generate` qui va générer un nombre de maisons différentes sur une grille.

Il est possible de générer un nombre de toits différents en créant un objet vide et en ajoutant le script `RoofSpawner`.

On peut aussi jouer avec les paramètres du toit d'un objet possédant le script `RoofGenerator` (attention à cocher *autoUpdate*)

Les scripts [House,Roof,Beam]Generator sont les composants d'une maison (on peut faire varier leurs paramètres), alors que les [House,Roof,Story]Spawner génèrent un nombre de composants aléatoires.