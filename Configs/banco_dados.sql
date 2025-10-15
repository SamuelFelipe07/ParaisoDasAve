CREATE DATABASE paraiso_das_aves;
USE paraiso_das_aves;


CREATE TABLE Cliente (
    id_cli INT AUTO_INCREMENT PRIMARY KEY,
    nome_cli VARCHAR(255),
    cpf_cli VARCHAR(20),
    data_nascimento_cli Varchar(300),
    rg_cli VARCHAR(20),
    bairro_cli VARCHAR(100),
    rua_cli VARCHAR(255),
    numero_cli VARCHAR(20) null,
    cep_cli VARCHAR(20),
    estado_cli VARCHAR(50),
    cidade_cli VARCHAR(50)
);

CREATE TABLE Animal (
    id_ani INT AUTO_INCREMENT PRIMARY KEY,
    nome_ani VARCHAR(255),
    raca_ani VARCHAR(100),
    idade_ani INT,
    especie_ani VARCHAR(100),
    porte_ani VARCHAR(50),
    peso_ani DECIMAL(5,2),
    diagnostico_ani TEXT,
    sexo_ani VARCHAR(10),
    id_cli_fk INT,
    FOREIGN KEY (id_cli_fk) REFERENCES Cliente(id_cli)
);

CREATE TABLE Funcionarios (
    id_fun INT AUTO_INCREMENT PRIMARY KEY,
    nome_fun VARCHAR(255),
    cpf_fun VARCHAR(20),
    rg_fun VARCHAR(20),
    data_nascimento_fun varchar(500),
    telefone_fun VARCHAR(20),
    email_fun VARCHAR(255),
    bairro_fun VARCHAR(100),
    rua_fun VARCHAR(255),
    numero_fun VARCHAR(20),
    conta_bancaria_fun VARCHAR(100),
    sexo_fun VARCHAR(20)
);



CREATE TABLE Fornecedores (
    id_forn INT AUTO_INCREMENT PRIMARY KEY,
    nome_forn VARCHAR(255),
    cnpj_forn VARCHAR(20),
    razao_social_forn VARCHAR(255),
    data_criacao_forn varchar(300),
    telefone_forn VARCHAR(20),
    email_forn VARCHAR(255),
    bairro_forn VARCHAR(100),
    rua_forn VARCHAR(255),
    numero_forn VARCHAR(20),
    cep_forn VARCHAR(20),
    estado_forn VARCHAR(50),
    cidade_forn VARCHAR(50)
);


CREATE TABLE Produto (
    id_pro INT AUTO_INCREMENT PRIMARY KEY,
    nome_pro VARCHAR(255),
    descricao_pro VARCHAR(350),
    quantidade_pro INT,
    marca_pro varchar(500),
    preco_pro float

);

CREATE TABLE Estoque (
    id_est INT AUTO_INCREMENT PRIMARY KEY,
    quantidade_est INT,
    validade_est VARCHAR(300),
    id_pro_fk int,
    id_forn_fk int,
    foreign key (id_pro_fk) references Produto(id_pro),
    foreign key (id_forn_fk) references Fornecedores(id_forn)
);

CREATE TABLE GestaoFinanceira (
    id_fin INT AUTO_INCREMENT PRIMARY KEY,
    emissao_nota_fiscal_fin VARCHAR(255),
    pagamento_funcionario_fin VARCHAR(255),
    contas_pagar_fin VARCHAR(255),
    contas_receber_fin VARCHAR(255),
    integracao_metodo_vendas_fin VARCHAR(255)
);

CREATE TABLE GestaoVenda (
    id_ven INT AUTO_INCREMENT PRIMARY KEY,
    quantidade_ven INT,
    data_venda_ven DATE,
    forma_pagamento_ven VARCHAR(50),
	id_pro_fk INT,
    id_fun_fk INT,
	id_cli_fk INT,
    FOREIGN KEY (id_cli_fk) REFERENCES Cliente(id_cli),
    FOREIGN KEY (id_pro_fk) REFERENCES Produto(id_pro),
    FOREIGN KEY (id_fun_fk) REFERENCES Funcionarios(id_fun)
);

CREATE TABLE GestaoCompra (
    id_com INT AUTO_INCREMENT PRIMARY KEY,
    quantidade_com INT,
    despesa_com DECIMAL(10,2),
    data_compra_com DATE,
	id_pro_fk INT,
    id_forn_fk INT,
    FOREIGN KEY (id_pro_fk) REFERENCES Produto(id_pro),
    FOREIGN KEY (id_forn_fk) REFERENCES Fornecedores(id_forn)
);

CREATE TABLE GestaoServicos (
    id_ser INT AUTO_INCREMENT PRIMARY KEY,
    id_fun_fk INT,
    descricao_ser  VARCHAR(500),
    tipo_ser VARCHAR(100),
    valor_ser FLOAT,
    id_cli_fk INT,
    FOREIGN KEY (id_fun_fk) REFERENCES Funcionarios(id_fun),
    FOREIGN KEY (id_cli_fk) REFERENCES Cliente(id_cli)
);


CREATE TABLE ContasAReceber (
    id_car INT AUTO_INCREMENT PRIMARY KEY,
    descricao_car VARCHAR(255),
    valor_car DECIMAL(10,2),
    data_car DATE,
    id_cli_fk INT,
    id_fun_fk int,
    forma_pagamento_car VARCHAR(50),
    FOREIGN KEY (id_cli_fk) REFERENCES Cliente(id_cli),
	FOREIGN KEY (id_fun_fk) REFERENCES Funcionarios(id_fun)
);

CREATE TABLE ContasAPagar (
    id_cap INT AUTO_INCREMENT PRIMARY KEY,
    descricao_cap VARCHAR(255),
    valor_cap DECIMAL(10,2),
    data_prevista_pagamento_cap DATE,
    data_vencimento_compra_cap DATE,
    id_forn_fk INT,
    FOREIGN KEY (id_forn_fk) REFERENCES Fornecedores(id_forn)
);
CREATE TABLE AdministracaoCaixa (
    id_caixa INT AUTO_INCREMENT PRIMARY KEY,
    saida_pagamento_funcionario_caixa VARCHAR(255),
    saida_reposicao_estoque_caixa VARCHAR(255),
    comprovantes_venda_caixa TEXT,
    comprovantes_compra_caixa TEXT,
    emissao_relatorios_caixa TEXT,
    opcao_pagamento_caixa VARCHAR(50),
    situacao_pagamento_caixa VARCHAR(50),
    compras_caixa VARCHAR(255),
    contas_bancos_caixa VARCHAR(255),
    id_fin_fk int,
    id_cap_fk int,
    id_car_fk int,
	FOREIGN KEY (id_cap_fk) REFERENCES ContasAPagar(id_cap),
	FOREIGN KEY (id_fin_fk) REFERENCES gestaoFinanceira(id_fin),
	FOREIGN KEY (id_car_fk) REFERENCES ContasAReceber(id_car)
);

CREATE TABLE Agendamento(
id_age int primary key auto_increment,
data_age varchar(300),
hora_age varchar(300),
id_ser_fk int,
foreign key (id_ser_fk) references GestaoServicos(id_ser)
);


-- CLIENTES
INSERT INTO Cliente (nome_cli, cpf_cli, data_nascimento_cli, rg_cli, bairro_cli, rua_cli, numero_cli, cep_cli, estado_cli, cidade_cli)
VALUES 
('Jo�o Pereira', '11122233344', '1988-05-10', 'MG1234567', 'Centro', 'Rua A', '100', '30100-000', 'MG', 'Belo Horizonte'),
('Ana Costa', '55566677788', '1995-11-22', 'SP7654321', 'Jardim', 'Rua B', '200', '04500-000', 'SP', 'S�o Paulo');

select * from cliente;
-- FUNCION�RIOS
INSERT INTO Funcionarios (nome_fun, cpf_fun, rg_fun, data_nascimento_fun, telefone_fun, email_fun, bairro_fun, rua_fun, numero_fun, conta_bancaria_fun, sexo_fun)
VALUES
('Carlos Silva', '99988877766', 'RJ1112223', '1990-02-15', '(21)91234-5678', 'carlos.silva@email.com', 'Copacabana', 'Rua C', '300', 'Banco XYZ 1234-5', 'Masculino'),
('Mariana Lima', '44433322211', 'BA3334445', '1992-07-30', '(71)97654-3210', 'mariana.lima@email.com', 'Barra', 'Rua D', '400', 'Banco ABC 9876-5', 'Feminino');



select * from fornecedores;
select * from produto;

-- FORNECEDORES
INSERT INTO Fornecedores (nome_forn, cnpj_forn, razao_social_forn, data_criacao_forn, telefone_forn, email_forn, bairro_forn, rua_forn, numero_forn, cep_forn, estado_forn, cidade_forn)
VALUES
('PetDistribuidora', '12345678000199', 'PetDistribuidora Ltda', '2010-03-15', '(31)90000-1111', 'contato@petdist.com', 'Gl�ria', 'Rua E', '500', '30120-000', 'MG', 'Belo Horizonte'),
('VetSuprimentos', '98765432000155', 'VetSuprimentos SA', '2015-08-25', '(11)90000-2222', 'contato@vetsup.com', 'Morumbi', 'Rua F', '600', '04510-000', 'SP', 'S�o Paulo');

-- PRODUTOS
INSERT INTO Produto (nome_pro, quantidade_pro, preco_pro, marca_pro, descricao_pro)
VALUES
('Ra��o para C�es', 30, 120.50, 'AA', 'Ra��o balanceada para c�es adultos'),
('Vacina Gatos', 10, 45.00, 'BB', 'Vacina anual para gatos');

-- ANIMAIS
INSERT INTO Animal (nome_ani, raca_ani, idade_ani, especie_ani, porte_ani, peso_ani, diagnostico_ani, sexo_ani, id_cli_fk)
VALUES
('Rex', 'Labrador', 4, 'C�o', 'Grande', 28.5, 'Saud�vel', 'Macho', 1),
('Luna', 'Siames', 2, 'Gato', 'Pequeno', 3.8, 'Sem problemas', 'F�mea', 2);

-- GEST�O FINANCEIRA
INSERT INTO GestaoFinanceira (emissao_nota_fiscal_fin, pagamento_funcionario_fin, contas_pagar_fin, contas_receber_fin, integracao_metodo_vendas_fin)
VALUES
('Emitida', 'Pago', 'Em atraso', 'Recebido', 'PIX, Cart�o'),
('Emitida', 'Pendente', 'A pagar', 'A receber', 'Dinheiro, PIX');

-- GEST�O VENDA
INSERT INTO GestaoVenda (quantidade_ven, data_venda_ven, forma_pagamento_ven, id_pro_fk, id_fun_fk, id_cli_fk)
VALUES
(2, '2025-10-08', 'PIX', 1, 1, 1),
(1, '2025-10-09', 'Dinheiro', 2, 2, 2);

-- GEST�O COMPRA
INSERT INTO GestaoCompra (quantidade_com, despesa_com, data_compra_com, id_pro_fk, id_forn_fk)
VALUES
(5, 602.50, '2025-09-30', 1, 1),
(3, 135.00, '2025-10-01', 2, 2);

-- GEST�O SERVI�OS
INSERT INTO GestaoServicos (id_fun_fk, descricao_ser, tipo_ser, valor_ser, id_cli_fk)
VALUES
(1, 'Consulta rotina', 'Consulta', 180.00, 1),
(2, 'Banho e tosa', 'Higiene', 90.00, 2);

-- CONTAS A PAGAR
INSERT INTO ContasAPagar (descricao_cap, valor_cap, data_prevista_pagamento_cap, data_vencimento_compra_cap, id_forn_fk)
VALUES
('Compra de Ra��o', 1500.00, '2025-10-15', '2025-10-10', 1),
('Compra de Vacinas', 450.00, '2025-10-20', '2025-10-18', 2);

-- CONTAS A RECEBER
INSERT INTO ContasAReceber (descricao_car, valor_car, data_car, id_cli_fk, id_fun_fk, forma_pagamento_car)
VALUES
('Servi�o Veterin�rio', 200.00, '2025-10-08', 1, 1, 'Cart�o'),
('Produto vendido', 120.50, '2025-10-09', 2, 2, 'PIX');

-- ADMINISTRA��O CAIXA
INSERT INTO AdministracaoCaixa (saida_pagamento_funcionario_caixa, saida_reposicao_estoque_caixa, comprovantes_venda_caixa, comprovantes_compra_caixa, emissao_relatorios_caixa, opcao_pagamento_caixa, situacao_pagamento_caixa, compras_caixa, contas_bancos_caixa, id_fin_fk, id_cap_fk, id_car_fk)
VALUES
('Pago', 'Reposi��o feita', 'Venda #1', 'Compra #1', 'Relat�rios emitidos', 'PIX', 'Conclu�do', 'Compra de ra��o', 'Banco 1', 1, 1, 1),
('Pendente', 'Planejada', 'Venda #2', 'Compra #2', 'Relat�rios pendentes', 'Dinheiro', 'Aberto', 'Compra de vacinas', 'Banco 2', 2, 2, 2);

select * from estoque;

INSERT INTO Estoque (quantidade_est, validade_est,  id_pro_fk, id_forn_fk)
VALUES
(50, '2026-12-31',  1, 1),
( 20, '2025-11-30',  1, 2);

-- Inserção 1
INSERT INTO Agendamento (data_age, hora_age, id_ser_fk)
VALUES ('2025-10-15', '14:30', 1);

-- Inserção 2
INSERT INTO Agendamento (data_age, hora_age, id_ser_fk)
VALUES ('2025-10-20', '09:00', 2);



