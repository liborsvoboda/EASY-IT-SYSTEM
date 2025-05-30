
Constraint is used to define rules on what values are inserted in the table. Constraint plays an important role in ensuring the accuracy and integrity of the data inserted into a table.

Commonly used constraints are as follows:
## 1. NOT NULL 
If a column is specified as NOT NULL while defining the table, then the column must not have NULL values while inserting data into the table.

### Syntax

```sql
create table table_name(column_name data_type not null,..) 
```
## 2. UNIQUE
 If a column is specified as UNIQUE while defining the table, then the column must have different values for all the rows present in the table.

 ### Syntax

```sql
create table table_name(column_name data_type not null unique,..) 
```
## 3. PRIMARY KEY 
Primary key is a column or set of columns which is used to uniquely identify a row in the database table. In the below customer table, You can either choose InsuranceID or National Insurance Number(NIN) as primary keys, but Insurance ID is preferable as NIN can be considered as personal information.
#### Table 1: 
    customer(InsuranceID, Name, DOB, NIN, Location)
#### Table 2: 
    patient(HospitalID, Name, DOB, InsuranceID)

### Syntax

```sql
create table table_name(column_name data_type not null,..,
primary key(column_name)) 
```
## 4. FOREIGN KEY 
Foreign key is a column in one table which references a primary key in other table. In the above two tables, InsuranceID of patient table can be foreign key which can be referenced to InsuranceID in customer Table.

### Syntax
```sql
create table table_name(column_name data_type,...,constraint 
constraint_name foreign key (column_name)  
references ref_table_name (ref_column_name)) 
```
## 5. CHECK 
checks for a specific condition while inserting data into the table. 

### Syntax
```sql
create table table_name(column_name data_type,..,  
primary key (),                                                       
constraint  check (condition))
```
### 6. DEFAULT 
If a value is not specified for a column whose default value is mentioned while creating the table, then the default value is inserted while inserting data.

### Syntax

```sql
create table table_name(column_name data_type DEFAULT value,..) 
```
Note:
1. If value is not specified then default values are as below for these data types
    * Numeric columns : 0
    * character or graphic fixed-length strings: blank
    * binary fixed-length strings: a set of hexadecimal zeros
    * datetime columns: current value of the associated special register
2. Value can be any of the below values as the default value:
    * a constant
    * NULL
    * SESSION_USER
    * CURRENT_SQLID
    