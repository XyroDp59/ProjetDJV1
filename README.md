# ProjetDJV1
Projet scolaire de développement d'un jeu vidéo type TPS

Ayant relu les consignes de rendu du projet que très tardivement, j'ai oublié de faire un repository git en amont.
Pour combler ce maanque, et pour vous prouver ma bonne foi, l'ordre dans lequel j'ai travaillé.

# Historique :
- Ayant eu l'idée de faire une parodie de la quête du ranch Romani de Zelda Majora's Mask, j'ai téléchargé un asset du Unity Store. J'ai rajouté quelques volumes pour faire un canon au dos de celle-ci.
- J'ai ré-utilisé le script de "orbital camera" du premier TP, que j'ai adapté pour qu'elles soient au dessus de l'épaule du joueur. Le zoom n'était pas encore implémenté.
- Ayant eu l'idée de vouloir gérer les ennemis par vague via un vaisseau faisant apparaître les ennemis, j'ai commencé par créer sa logique : il se déplace aléatoirement, et fait apparaître des ennemis. J'ai eu quelques soucis pour qu'il s'arrête exactement là où je le souhaitais, du a des incertitudes : j'ai du détecter s'il était à moins d'une distance minimmale. 
- Puisque l'on aurait beaucoup d'ennemis, plutôt que de faire apparaître un ennemi à la fois, j'ai décider de partir sur une pool, contenant une cinquantaine d'Alien.
- Les Alien tout comme le joueur et le vaisseau sont régis par une quantité de point de vie, et peuvent prendre des dégats : j'ai créé un script HealthSystem, permettant d'alléger leur 3 scripts, et éviter un copier coller. Je n'avais pas encore fait d'UI donc il n'y avait pas encore les barres de vies.
- J'ai écrit le script Harmful Projectile : il est nommé ainsi plutôt que bullet, car j'avais comme intention de mettre plusieurs types d'armes pour le joueur. Cette idée a été abandonnée.
( En raison de divers projets impérieux et partiels, je n'ai pas travaillé sur le projet entre la semaine avant les vacances et le 05 Janvier )
- J'ai créé le prefab des Alien Kidnappeurs : il fallait donc créer leur script. Il a donc fallu aussi créer les particules du kidnapping, et les vaches et leur script de mort. J'ai eu quelques problèmes à bien positionner les particules lors du kidnapping. 
- J'ai enfin créé le script de déplacement et saut du joueur. Cela n'a pas posé de problème, si ce n'est pour le tir des balles : il fallait faire en sorte qu'elles sortent du canon, mais visent le point au centre de l'écran. En m'inspirant du script pour déplacer le joueur à la souris que l'on avait fait dans les TPs, j'ai pu récupérer ce point.
- J'ai dessiné l'arène. Je l'ai un peu retravaillé les jours qui ont suivi, pour la rendre plus agréable (par exemple en créant des murs invisibles à ses bords), mais en gardant la même base.
- J'ai ensuite créé le prefab des Alien Shooter.
- J'ai créé des barres de vies, en me basant sur le TP. J'ai relié les HealthSystem aux barres de vies.
- J'ai ensuite créé les Heal, à savoir les bouteilles de lait.
- N'étant pas très fan du ressenti du jeu, j'ai retravaillé la caméra : en effet, la caméra suivait bizarrement la souris, et son comportement ne s'adaptait pas lorsque la souris était au centre de l'écran.
- J'ai fini le HUD en ajoutant des scores pour la mort des ennemis, le nombre de vagues, et le nombre de vaches (nos amis) encore en vie.
- J'ai ajouté un Game-Over, qui peut être déclenché par la mort du joueur, ou par la mort de la dernière vache.
- Ayant toujours le problème de la caméra passant à travers les murs, j'ai ajouté une fonction de zoom à celle-ci : la caméra essaiera de se mettre en première personne si elle n'arrive pas à voir le joueur à l'écran.

- Création du Git et initial commit

- Ajout de la scène écran titre
- envoi du projet
