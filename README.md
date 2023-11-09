# Hochet

## Comment lancer le projet :
- Cloner ou télécharger le projet 
- Ouvrir le dossier *hochet* téléchargé comme projet dans Unity (version 2021.3.x)
- Choisir une des scènes dans le dossier Assets/Scenes/3A2023 Scenes et l'ouvrir avec un double clic
- Lancer la scène
Pour ouvrir le projet en VR, il faut connecter le casque avec l'application Oculus avant de lancer la scène.

Pour plus de détails sur l'utilisation de l'application voir la [Documentation utilisateur](Documentation/DocUtilisateur.pdf).
Pour plus de détails sur le fonctionnement technique de l'application voir la [Documentation technique](Documentation/DocTechnique.pdf).






## (09/2022) En vrac pour SEVEN :
Bug d'éditeur : difficilement reproducible on purpose, mais par moment lorsque l'on clique sur une transition, la moitié droite où doit être affiché tous les paramètres est juste grisée (seulement le fond de l'éditeur est visible) et rien n'est modifiable. Il faut supprimer la transition et la remettre pour regler le pb

Edit : il est (fort) possible qu'une des raisons du proc de ce bug viennent d'une modification d'un effector (ou sûrement d'un sensor aussi, mais seulement "testé" avec un effector atm) utilisé dans un scénario. Ce bug s'est produit sur toutes les transitions utilisant notre effector "PlaySoundAndShowSubtitles" après que Liam est rendu les 2 premiers fields optionnal



Bug d'appel d'effector (qui n'a aucun effet visiblement à part spam le log d'erreurs) :
Testé avec PlaySoundAndShowSubtitles.
Si une scène est unload pendant l'execution d'un effector, il est possible qu'il envoie un message d'erreur en boucle indiquant des références null. Bug reproducible en lançant l'appli avec la Crafting scene d'ouverte (Crafting scene -> première transi = PlaySoundAndShowSubtitles -> unload cash car script de Preload scene -> loop log.error)
