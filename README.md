# Emergent Economies for RPGs
This is a work in progress implementation of "Emergent Economies for Role Playing Games".

## Current classes

### 1. Agent
- Central in the simulation
- It is the person who buy, sell and produce...
### 2. Commodity
- Store data of one commodity.
- It can not be changed.
- This unique data by commodity is used globally as reference by the other classes.
### 3. Commodities
- Store data of all commodities. It's like a SQL Table "Commodities". 
- It ensures that commodities won't be duplicated, will have unique ids and will be accessible by the other classes. 
### 4. InventoryItem
- Stores data of one item in the inventory. It points to (reference) one unique **Commodity**. 
- Since each **Agent** have your own collection of inventory itens, two instances of **InventoryItem** can point to the same **Commodity**, but they will be part of different instances of **Inventory** (different instances of **Agent**).
### 5. Inventory
- The collection of commodities of each **Agent**. Each **Agent** has your own **Inventory**.
- For instance, it gives the **Agent** access to inventory item levels (amount).
### 6. Role
- It is the profession.
- Each **Agent** has one **Role**.
- Each **Role** has unique production rules, that requires some combination of materials to create new ones.
### 7. RoleCommodities
- Stores specific rules for each commodity inside a **Role**
- Example: *Wood* is produced by the role **Woodcutter**, but consumed by the role **Blacksmith**. So, the **Commodity** *Wood* will be treated different by each of these roles.

