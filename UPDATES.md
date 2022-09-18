# Updates

### [Sep 11]

**DONE**

- Finished the reading of json files. Now, the production rules for each role are defined in json files, with is easier to change and manage. 

**Next Step**

- Integrate this data that is readed from json files and is ready in memory into the program flow.
- Previous logic used two levels of production (full and not full). That will be changed for a more dynamic logic.
- I think i will be working more with Inventory, Role, RoleCommodity and RoleCommodities entities.

### [Sep 17]

**Done**
- Integrate the **RoleProductionRules** into the program
- Code looks shorter and cleaner
- Remove the restriction of two levels of production. Now this is dynamic and can be managed direct from the json files.
- Removed **RoleCommodity** and **RoleCommodities** classes. They are not needed anymore.
- Updated json files to previous values combinations used.

**Next Step**
- In the **generateOneOffer** method from **Agent** need to handle amount beyond the threshold for bid types.
- And next, I should start the plots. I think I will use excel, to be fast. If it is not enough, I can use R. The idea is to have a fast way to start to check and analyze results.