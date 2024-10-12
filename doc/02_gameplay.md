# Gameplay
- Roguelike où l'on peut surcharger son personnage à l'aide de stimulants pour le rendre plus fort.
- L'impact souhaité est d'inclure une mécanique de jeu simulant l'addiction aux substances et de la mettre en scène de manière à ce que le joueur ait la tentation de consommer toujours plus. Puis de réaliser plus tard cette dépendance, en ayant la tentation d'abuser de cette consommation dans le jeu.
- Le joueur enchaîne une série de salles l'une après l'autre, contenant à chaque fois un ennemi à abattre.
- Un ennemi est une représentation de challenge dans la vie. Par exemple le stress, l'anxiété, les responsabilités, etc. Il est défini par un type le définissant, qui modifier les dégâts de ses attaques qui blessent l'une des caractéristique du joueur. Ils deviennent plus forts / compliqués à vaincre plus le joueur avance dans les salles.
- À tout moment, le joueur a la possibilité de consommer des stimulants pour augmenter ses caractéristiques lors d'un certain nombre de salles. Les stimulants induisent des effets négatifs sur le long terme.
- Types de stimulants:
	- Cigarette
	- Alcool
- Caractéristiques du joueur:
	- Santé
	- Niveau de stress
	- Niveau d'accoutumance à la cigarette
	- Niveau d'accoutumance à l'alcool
- Actions du joueur:
	- WASD pour se déplacer.
	- Clique de souris pour attaquer (laisser appuyé pour attaquer en continu) dans la direction du curseur.
	- Q: consommer de l'alcool
	- E: consommer une cigarette
	- Espace pour dodge (donne au joueur quelques frames d’invincibilité + un petit dash dans la direction du personnage).
- Le game loop consiste en:
	1. On arrive dans une salle contenant un ennemi à abattre. Une fois battu, on gagne des points et de l'argent en récompense.
	2. Un distributeur apparaît au centre de la salle qui nous permet d'acheter des stimulants, packs de soins, etc.
	3. Enfin on sélectionne la salle suivante parmi les 2-3 possibles. En fonction du niveau d'intelligence, le joueur peut apercevoir le type d'ennemi qu'elle contient.
	4. Puis la boucle 1-3 recommence.
	5. La partie fini quand l'une des caractéristiques de joueur arrive à zéro.
- on enchaîne quelques salles qui aboutisse à un boss pour faire une run compète