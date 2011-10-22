Simple tool to generate dummay data (members, Campaigns and donations) for GiveCRM
Usage:
-  Run it
- Click one of
  - Generate members. The default number of mebers to generate is 10 000. This takes about a minute. It has been tested to scale OK up to 100 000 members.
  - Load members. Load existing members from the database. Loading 100 000 mebers takes around 20 seconds.
  
 - Generate campaign. Make a new campaign.
 
 - Generate donations. (Must have members and campaign first). Between 33% and 66% of the loaded members have a campaign donation generated against them.
 
 
 At present the only way to control which database is used is to edit the app.config file, and configure the connection called "GiveCRM". This databse will be used.