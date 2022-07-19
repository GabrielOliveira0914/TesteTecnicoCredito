create database Financiamento

use Financiamento

create table Cliente(
nome_cliente varchar (60),
cpf_cliente varchar (11),
uf_cliente varchar (20),
celular varchar (14),
PRIMARY KEY (cpf_cliente)
);

create table Financiamento(
id_financiamento int IDENTITY (1,1),
cpf_cliente varchar (11),
tipo_financiamento varchar (60),
valorTotal_financiamento float (10),
dataUltimoVenc_financiamento date,
PRIMARY KEY(id_financiamento),
FOREIGN KEY (cpf_cliente) REFERENCES Cliente(cpf_cliente) 
);


create table Parcela(
id_parcela int IDENTITY (1,1),
id_financiamento int,
numero_parcela int,
valor_parcela float (10),
dataVencimento_parcela date,
dataPagamento_parcela date,
PRIMARY KEY (id_parcela),
FOREIGN KEY (id_financiamento) REFERENCES Financiamento(id_financiamento)
);


insert into Cliente values ('Bianca Vitoria', '12312312312','SP','11923456789')

insert into Financiamento (cpf_cliente, tipo_financiamento, valorTotal_financiamento,dataUltimoVenc_financiamento) values ('12312312312', 'consignado', 100000, GETDATE()+60)
insert into Financiamento (cpf_cliente, tipo_financiamento, valorTotal_financiamento,dataUltimoVenc_financiamento) values ('00-0753526', 'Imobiliario', 100000, GETDATE()+60)

select c.* from Cliente c
left join financiamento f on 
c.cpf_cliente = f.cpf_cliente
where c.uf_cliente = 'SP' 
and f.id_financiamento in(
select id_financiamento from Parcela
where
((select count(id_financiamento) from Parcela group by id_financiamento) / (select count(id_parcela) from Parcela where dataPagamento_parcela is not null group by id_financiamento))*100 >= 60
group by id_financiamento  )

select top 4 c.* from cliente c
left join Financiamento f on
c.cpf_cliente = f.cpf_cliente
where f.id_financiamento in (
select id_financiamento from Parcela
where dataVencimento_parcela < getdate()-5
and dataPagamento_parcela is null)