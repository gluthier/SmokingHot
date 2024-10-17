## Gameplay v2
#### Résumé & impact
- Roguelite où l'on peut surcharger son personnage en fumant des cigarettes pour le rendre plus fort.
- Le joueur incarne quelqu'un qui souffre d'addiction à la cigarette et essaie de lutter au mieux pour avancer dans sa vie.
- L'impact souhaité est d'inclure une mécanique de jeu simulant l'addiction à la cigarette, et de la mettre en scène de manière à ce que le joueur ait la tentation de fumer toujours plus. Puis de réaliser plus tard cette dépendance, en ayant la tentation d'abuser des cigarettes dans le jeu. => on envie que le joueur puisse se mettre dans la peau de quelqu'un souffrant d'addiction pour se rendre compte des difficultés de lutter contre.
#### Nomenclature
-  Lors d'une **partie**, le joueur enchaîne une série de **sorties** pour y obtenir le meilleur score possible.
- Une **sortie** consiste en une série de **salles** l'une après l'autre, contenant des **ennemis** à abattre aboutissant à une dernière salle plus difficile contenant un **boss**.
#### Joueur
- **Santé**: à 0 on meurt
- **Endurance**: pour sprinter, éviter les coups (dodge)
- **Stress**: augmente lors d'actions qui font peur (commencer une nouvelle sortie, se faire toucher par un ennemi, quelques attaques spéciales de boss) => donne envie de fumer pour se calmer
- **Addiction** à la cigarette: augmente en fumant, induit des effets négatifs sur le long terme
#### Ennemis et Boss
- Les ennemis sont assez simple à battre. Ils permettent de monter l'intensité des combats de la sortie et d'introduire les mécaniques du boss (s'il y en a) et ainsi de s'y familiariser.
- Un boss est une représentation de challenge dans la vie. Par exemple le stress, l'anxiété, les responsabilités, etc. Ils deviennent plus forts / compliqués à vaincre plus le joueur avance dans les sorties.
- Les ennemis et les boss sont influencés par la consommation du joueur (plus on fumer et consomme de l'alcool, plus ils deviennent forts).
#### Consommations (cigarette et alcool)
- À tout moment, le joueur a la possibilité de fumer des cigarette pour augmenter ses caractéristiques positivement à cours terme, mais ça induit aussi des effet négatifs sur le long terme.
- De temps en temps lors d'une salle, et à des moments clés lors d'une bataille contre un boss, des bouteilles d'alcools apparaissent sur la carte. Passer dessus permet de les consommer pour en obtenir des effets positifs (soin de la santé, réduction du stress), mais augmente les effets de l'addiction à la cigarette.
#### Game Loop
1. On est dans notre mind palace (== safe zone), on peut voir nos stats et sélectionner la prochaine sortie (on peut voir quel boss final elle contient) puis la commencer.
2. On arrive dans la première salle de la sortie, contenant des ennemis à abattre. De temps en temps des bouteilles d'alcool apparaissent sur la carte, qui nous donne des bonus si on passe par dessus. On peut aussi fumer une cigarette pour se booster. Une fois battu, on gagne des points en récompense et on entre dans la salle suivante.
3. On les enchaîne puis on arrive dans la salle finale de la sortie contenant un boss. Après quelques paliers de vie du boss (76%, 50%, 25%), des bouteilles d'alcool apparaissent.
4. Puis on recommence.
La partie fini à la fin des séries de sorties prédéfinies (à choisir combien, en fonction des boss qu'on implémente) (== victoire) ou quand la santé du joueur tombe à zéro (== défaite).
## Core mechanics v2
#### Joueur
- Stats du joueur:
    - Santé: 200 HP
    - Endurance: 10/10
    - Niveau de stress: 0/10 (au début du jeu)
    - Addiction aux cigarettes: 5/10 (au début du jeu)
    - Vitesse d'attaque: 10 (équivaut à 0.5 attaque par seconde)
    - Dégâts d'attaque: -10 HP
    - Vitesse de déplacement: XX
- Actions du joueur:
	- WASD pour se déplacer.
	- Clique gauche de souris pour attaquer (laisser appuyé pour attaquer en continu) dans la direction du curseur.
	- Q (ou autre): fumer une cigarette.
	- E (ou autre): interaction (par exemple consommer de l'alcool une fois qu'on est dessus)
	- Espace pour dodge (donne au joueur quelques frames d’invincibilité + un petit dash dans la direction du personnage) (consomme un montant de endurance).
	- Left Shift (laisser appuyé): sprinter (consomme continuellement de l'endurance).
#### Cigarettes
- Les cigarettes sont illimitées.
- Effets de consommation de cigarette:
	- -1 stress (-2 si on a consommé au moins une fois de l'alcool dans la salle)
	- +10 HP (+20 si on a consommé au moins une fois de l'alcool dans la salle)
	- +1 vitesse d'attaque
	- -1 endurance
	- (nombre de cigarette consommée dans la sortie / 2 + 1) accoutumance cigarette
- Effet visuel:
	- effet de vignette qui assombri les bords de l'écran. En re-fumant, les effets visuels disparaissent, mais reviennent ensuite plus vite et plus fort.
- Effet audio:
	- son stylisé d'inhalation 
#### Alcool
- Les bouteilles (canettes) d'alcool apparaissent de temps en temps sur la map, et à des paliers de points de vie du boss (75%, 50%, 25%).
- Il faut passer par dessus et appuyer sur la touche interaction pour en consommer.
- Effets de consommation d'alcool:
	- -2 stress
	- +20 HP
	- +1 dégâts d'attaque
	- -2 endurance
	- Réduction de precision des attaques pendant 10 secondes
- Effet visuel:
	- l'écran se trouble pendant 10 secondes
- Effet audio:
	- son stylisé de canette qui s'ouvre
#### Stress
- Niveau de stress:
	- +5 par sortie commencée
	- +2 par coup de boss reçu
	- +1 par coup d'ennemi reçu
	- +1 chaque 30 secondes sans cigarette fumée
	- -1 par cigarette fumée
	- -2 par alcool consommé
	- -3 par salle sans cigarette fumée
- Effets du stress:
	- Plus le niveau de stress est élevé, moins les attaques sont précises (elles ne vont pas toujours droit dans la direction du curseur)
	- les ennemis + boss deviennent plus forts plus le niveau de stress augmente
    - si niveau de stress >8: on a 5% de chance chaque 5 secondes de rester figé sur place pendant 1 seconde
#### Addiction
- Chaque point d'addiction ajouté dépassant la limite supérieure = -1 HP par point
- QTE de manque (si addiction > 6): on a 3 secondes pour fumer une cigarette avec tous les effets doublés => tentation sous pression, sans le temps de réfléchir aux conséquences.
#### Ennemis et Boss
- Ennemi:
	- Entre 30 et 150 HP
	- Vitesse d'attaque: entre 0.25 et 1.5 attaques par seconde
	- Force: entre 1 et 3
	- Dégât d'attaque: -10HP * force
	- +1 stress par coût reçu
- Boss:
	- Entre 200 et 400 HP
	- Vitesse d'attaque: entre 0.5 et 3 attaques par seconde
	- Force: entre 3 et 6
	- Dégât d'attaque: -10HP * force
	- +2 stress par coût reçu
- **À définir**: les ennemis et boss réagissent à la consommation de cigarettes et d'alcool, et au niveau de stress.
#### Salles simple
- contient entre 1 et 4 ennemis
#### Salles finale
- contient un boss
#### Fin d'une salle
- -1 addiction cigarette si aucune cigarette consommée dans la salle
- +1 vitesse et dégâts d'attaque si accoutumance cigarette == 0
- si addiction cigarette > 8: consommation automatique d'une cigarette
#### Fin d'une sortie
- -5 HP max si addiction cigarette > 5
 - On récupère toute notre santé (HP = HP max)
 - Vitesse d'attaque reset
 - Dégâts d'attaque reset
 - Niveau de stress reste à sa valeur
 - Addiction cigarette reste à sa valeur