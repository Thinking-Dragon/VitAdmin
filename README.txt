Installation de VitAdmin :
	Extraire l'archive .zip
	Créer une base de donnée MySql
	Exécuter le script "vitadmin_bd_main.sql" (inclus dans l'archive) sur la base de donnée créée précédemment

Paramètrage
	Ouvrire le fichier /Configuration/ChainesConnexion.config
	Modifier l'attribut «connectionString=», à la ligne qui commence par «<add name="MySql"»
		Mettre les informations de votre serveur de base de donnée, dans le format suivant:
			«Server=[adresseIP]; database=[nomBD]; UID=[Usager]; password=[Mot de passe]; SslMode=none»
	Modifier l'attribut «connectionString=», à la ligne qui commence par «<add name="FTP"»
		Mettre les informations de votre serveur de Fichier, dans le format suivant:
			«connectionString="L'ALIAS DE MON SERVEUR FTP"»

Exécuter l'application
	Exécuter le fichier "VitAdmin.exe"

Usagers pour tester
   Un administrateur
	Nom : "Admin"
	Mot de passe : ""

   et
   
	Pour tester les écrans de gestions des dossiers d'un patient
   Un professionnel
	Nom : "TherienJ"
	Mot de passe : ""
   
   et
   	Pour tester l'écran de l'horaire de la semaine courante
   Un professionnel
	Nom : "Bas"
	Mot de passe : ""
	