# Brainstorm

## Simulateur d'urgences d'hôpital
- La volonté du jeu est de faire réaliser la complexité de gestion de patients dans les urgences d'un hôpital
- On contrôle la personne au guichet qui doit décider de l'ordre de passage des patients
- Chaque patient a un degré de gravité, durée de prise en charge et professionnel spécifique (par exemple neurologue, chirurgien, etc.) différent
- Les patients se déplacent et agissent de manière autonome en fonction des directive choisies (on peut imaginer que quelques-uns n'en fassent qu'à leur tête)
- Vue du dessus comme ça on peut voir l'état des différentes salles d'attentes et pièces de prise en charge des patients

## Depressed Day
- Le but est de se mettre dans la peau de quelqu'un de (cliniquement) déprimé pour se rendre compte de la difficulté d'effectuer n'importe quelle tâche de quotidien dont on n'a pense même plus en la faisant nous même
- Plan par pièce d'un appartement vue du dessus, chaque pièce est un niveau (scope: on commence avec une unique pièce qui pourrait être un salon + cuisine => tâches à faire à la cuisine, mais on n'arrive pas à sortir du canapé)
- Certaines tâches demandent un timing (cuisson d'un plat, aller au travail pour telle heure), mais tout mouvement devient plus lent, on devient plus "lourd"
- À la fin de chaque niveau, il y a un panneau explicatif résumant les difficultés des personnes souffrant de dépression ainsi qu'un encouragement à aller vite un professionnel si on en ressent les troubles
- Certaines tâches demandent des choix (quoi manger, quels habits mettre, quoi regarder à la TV), mais ils sont démultipliés
- Certaines tâches demandent une agilité/précision (découper les légumes, ranger les habits propres), mais les éléments sont en mouvement => régit par l'IA
- Certaines tâches demande de s'en souvenir (date de sortie avec des amis, sortir le container le bon jour), mais les dates de mélangent entre elles, certaines "disparaissent"

## Addiction drogue et alcool 
- Infos: https://www.betterhealth.vic.gov.au/health/servicesandsupport/alcohol-and-drugs--dependence-and-addiction
- Le but est de se mettre dans la peau de quelqu'un qui souffre d'une addiction aux drogues et/ou alcool afin de reconnaître les signes les plus prononcés.
- Suite de plans vue d'en haut (correspondant à un niveau), qui met en scene différentes situations de la vie courante où la consommation de substances pourrait être faite.
- Système de stats style RPG (cf. [skills dans Disco Elysium](https://discoelysium.fandom.com/wiki/Skills)) qui sont utilisées pour "valider" un challenge. La consommation d'une substance va booster temporairement les stats (on est donc tenté d'en prendre), mais induit sur le long terme une diminution de stat (donc on est tenté de consommer encore plus pour lutter contre).
- [Attributs dans Disco Elysium](https://discoelysium.fandom.com/wiki/Attributes):
	- Intellect: Raw brain power, how smart you are. Your capacity to reason.
	- Psyche: Sensitivity, how emotionally intelligent you are. Your power to influence yourself and others.
	- Physique: Your musculature, how strong you are. How well your body is built.
	- Motorics: Your senses, how agile you are. How well you move your body.
- [Drogues dans Disco Elysium](https://discoelysium.fandom.com/wiki/Drugs):
	- Nicotine: +1 intellect, -1 health
	- Pyrholidon (fictif): +1 psyche, -1 health
	- Alcohol: +1 physique, -1 morale
	- Amphetamine: +1 motorics, -1 morale
- **Scène 1**: le réveil: tests ultra faciles au début, mais suivant la dépendance et donc effets temporaires négatifs, peuvent devenir difficiles
	- se lever à l'heure pour le travail
	- choisir ses habits
	- manger
- **Scène 2**: au travail
	- accomplir ses tâches dans le temps
	- 1on1 avec le/la boss
	- rester en forme (éveillé)
	- maintenir de bonnes relations avec les collègues
- **Scène 3**: soirée à la maison
	- regarder la TV (trouver un moyen pour que la tentation d'un verre soit présente)
	- garder le moral
	- maintenir de bonnes relations avec la famille/amis
- 1-3: répétition pour lundi à vendredi (peut-être 5 répétitions ça fait trop)
- **Scène 4**: soirée du weekend
	- sociabiliser (tentation alcool)
	- s'amuser en soirée
- Répétitions des scènes pour se rendre compte des signes de dépendance lors de la consommation à la chaîne.

## Cluedo like
- Résoudre un meurtre en trouvant le/la coupable
- Game loop:
	- interroger des suspects pour découvrir des preuves
	- le dialogue peut nous demander de faire un "check" de stat qui fait avancer l'histoire si on le réussi
	- les stats peuvent être booster en fumant ou buvant
- Chaque décision difficile est facilitée en fumant une cigarette
	- l'histoire avance aussi si on est constamment stressé
- Stat: psyché qui est augmentée en fumant une cigarette ou en attendant un bon moment

### Signs of alcohol or other drug dependence
[Source](https://www.betterhealth.vic.gov.au/health/servicesandsupport/alcohol-and-drugs--dependence-and-addiction) Some signs that you may have an alcohol or other drug problem are:
- changed eating or sleeping habits
- caring less about your appearance
- spending more time with people who drink or use drugs to excess
- missing appointments, classes or work commitments
- losing interest in activities that you used to love
- getting in trouble in school, at work or with the law
- getting into more arguments with family and friends
- friends or family asking you if you use alcohol or other drugs
- relying on drugs or alcohol to have fun or relax
- having blackouts
- drinking or using drugs when you are alone
- keeping secrets from friends or family
- finding you need more and more of the substance to get the same feeling

## Brainstorm
- Nutrition:
	- Faire des plats de plus en plus compliqués
	- cf. le jeu overcooked
	- jouable à plusieurs
	- but: apprendre la nutrition, à cuisiner plus sain

## Life struggle
- Roguelike
- simulation accélérée de vie
- attention à ne pas être dans le jugement, dans le "juste" vs "faux"
- choix à faire déterminant le nombre de points qu'on fait
- but: gagner le plus de points possible => faire les meilleures choix possibles
- on peut consommer des stimulants (cigarettes, alcool, etc.) pour obtenir un boost temporaire (mais en contre-partie un autre effet négatif + simulation de dépendance sur le long terme)
- série de salles représentant une situation de vie (vignettes), avec plusieurs sorties possibles à choix (débloquée une fois la salle actuelle terminée) dont on a une vue limitée du challenge qu'elle comprend
- Salles comprenant des tests de force
	- il faut vaincre une représentation d'un élément négatif (vaincre l'angoisse, la dépression) => il nous faut un système d'attaque, par exemple des projectiles qui sont lancés automatique de manière rythmée (à la Binding of Isaac, mais sans forcement les upgrades des armes)
- Salles comprenant des tests de mobilité (résolu par du mouvement)
	- il faut fuir une menace (contrôle fiscale, accident de voiture) => on perd des points de vie
- Salles comprenant des tests "intellectuels"
- Salle comprenant des tests "psychiques" (résolu par un jet de dés)
	- il faut créer quelque chose (un poème pour notre partenaire, une recette de cuisine, un cadeau pour notre maman)
	- il faut accomplir un challenge (réussir un examen de conduite)
- avant de commencer chaque salle, on a un aperçu du challenge dans la suivante => moyen de se préparer, de booster son personnage
- après chaque salle il y a un selecta où on peut acheter des items (soin, boosts, etc.)
- on gagne de l'argent en réussissant les salles
- chaque boost est appliqué uniquement à la salle d'après, avec des effets négatifs qui durent plusieurs salles
- salles forces => représente combat personnel (lutte contre depression, angoisses, etc.)
- salles mentales => représente combat social, lien avec un.e partenaire,  famille, ami, collègues, etc. (?)
- chaque salle comprend un combat, suivie d'une discussion rapide avec l'ennemi comprenant un choix menant sur un test de dés 

### Stats
- Forme physique: attaques plus de dégâts, plus de points de vie 
- Motricité/agilité: mouvements plus rapides (déplacement et attaques)
- Intelligence/raisonnement: devine les challenges des salles suivantes
- Mentale/sensibilité: devine les patterns d'attaque des ennemis et les test de dés dans le dialogue
