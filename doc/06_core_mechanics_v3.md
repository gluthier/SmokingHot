# Core mechanics v3
- Durée d'une partie: 5 minutes
	- 1 minute = 10 ans dans le jeu simulés
	- au total: 50 ans de simulation
- Différentiation par continent (histoire de simplifier => par pays ça devient trop détaillé)

## Continents
- Europe (joueur)
- Afrique (IA)
- Amérique du nord (IA)
- Amérique du sud (IA)
- Australie (IA)

## Stats
- Entreprise:
	- Argent total
	- Image publique, par tranche d'âge
	- Quantité de campagnes publicitaires
	- Cigarettes produites:
		- Toxicité
		- Niveau d'addiction
		- Prix de production
		- Prix de distribution
	- Par continent et par tranche d'âge:
		- Nombre de paquets de l'entreprise vendus par an
		- Nombre de fumeurs de cigarettes de l'entreprise
		- => nombre de paquets fumé par personne = nb packets / nb personnes
		- Pourcentage d'acquisition de nouveaux fumeurs
		- Pourcentage de rétention de fumeurs
		- Parts de marché (vs autres entreprise gérée par l'IA)
- Continent:
	1. Prix du packet de cigarette
	2. Nombre de personnes:
		1. enfants (<11 ans)
		2. adolescents (11-16 ans)
		3. jeunes adultes (16-25 ans)
		4. adultes (25-65 ans)
		5. seniors (>65 ans)
	3. Par tranche d'âge:
		1. Nombre de fumeurs
		2. => nombre de non-fumeurs = nb personnes - nb fumeurs
		3. Nombre de packets vendus
		4. => nombre de packets fumé par personne = nb packets / nb personnes
		5. Taux de décès lié à la cigarette par an
- Campagne publicitaire:
	- Prix par an
	- Durée (un an ou +)
	- Public atteint:
		- tranche d'âge
		- fumeur ou non-fumeur
	- Qualité/réception: augmente les chances de réussite de la campagne
	- Type:
		- acquisition de nouveau fumeurs
		- rétention de fumeurs
- Événements:
	- Fréquence d'apparition
	- Type:
		- Politique
		- Publicité
		- R&D
	- Niveau d'impact (utile pour les stratégie de l'IA):
		- Positif
		- Négatif
	- Coût si accepté (argent ou autre) / conséquence
	- Coût si refusé (argent ou autre) / conséquence

## Simulation
- Chaque 6 secondes (chaque an simulé):
	- Gain argent: prix du packet * nombre de packets vendus
	- Dépense argent:
		- Nombre de publicités * leur durée * leur prix
	- Décès: nombres de personnes -= taux de décès * nombre de fumeurs
	- Nouveau fumeurs:
		- pourcentage d'acquisition * nombre de non-fumeurs
		- qualité de pub * public atteint * type de pub
	- Rétention de fumeurs:
		- pourcentage de rétention * nombre de fumeurs
		- qualité de pub * public atteint * type de pub
		
## Stratégies de l'IA
1. 90% Accepter tous les événements, 10% random
2. 90% Refuser tous les événements, 10% random
3. 90% Toujours choisir l'option la moins chère à court terme, 10% random
4. 90% Favoriser l'option avec le plus gros gain à long terme, 10% random