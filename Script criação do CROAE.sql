/* 
--------------------------------------------
	       Criar e Utilizar a DB
--------------------------------------------
*/
-- Drop database croae_projeto;
-- Criar base de dados
CREATE DATABASE IF NOT EXISTS croae_projeto;

-- Usar esta base de dados
USE croae_projeto;

/* 
--------------------------------------------
			Criar as Tabelas 
--------------------------------------------
*/

-- Criar as tabelas com chaves primárias e foreign keys 

DROP TABLE IF EXISTS racas;
CREATE TABLE racas (
    raca_id INT PRIMARY KEY AUTO_INCREMENT,
    raca VARCHAR(45) NOT NULL UNIQUE
);

DROP TABLE IF EXISTS alas;
CREATE TABLE alas (
  ala_id INT PRIMARY KEY AUTO_INCREMENT,
  tipo VARCHAR(50) UNIQUE
);

DROP TABLE IF EXISTS boxes;
CREATE TABLE boxes (
    box_id INT PRIMARY KEY AUTO_INCREMENT,
	ala_id INT,
    CONSTRAINT fk_boxes_alas FOREIGN KEY (ala_id) REFERENCES alas(ala_id) ON DELETE SET NULL
);

DROP TABLE IF EXISTS caes;
CREATE TABLE caes (
    cao_id INT PRIMARY KEY AUTO_INCREMENT,
    raca_id INT,
    nome VARCHAR(50) NOT NULL,
    data_nascimento DATE NOT NULL,
    porte ENUM('Pequeno', 'Médio', 'Grande') NOT NULL,
    sexo ENUM('M', 'F') NOT NULL,
    data_entrada DATETIME NOT NULL,
    cruzamento_raca VARCHAR(100),
    box_id INT,
    castrado boolean NOT NULL,
    caracteristica VARCHAR(100) NOT NULL,
    disponivel BOOLEAN NOT NULL DEFAULT TRUE,
    CONSTRAINT fk_caes_racas FOREIGN KEY (raca_id) REFERENCES racas(raca_id) ON DELETE SET NULL, -- nao quero apagar um cao só porque se apagou uma raça
    CONSTRAINT fk_caes_boxes FOREIGN KEY (box_id) REFERENCES boxes(box_id) ON DELETE SET NULL -- nao quero apagar um cao só porque se apagou uma box
);

/*DROP TABLE IF EXISTS doacoes_web;
CREATE TABLE doacoes_web (
	doacao_web_id INT PRIMARY KEY AUTO_INCREMENT,
    nome_doador VARCHAR(100) NOT NULL,
    objeto_doado VARCHAR(15) NOT NULL,
    quantidade_doada INT NOT NULL,
    valor_pago DOUBLE NOT NULL,
    descricao VARCHAR(100)
);*/

DROP TABLE IF EXISTS padrinhos;
CREATE TABLE padrinhos (
    padrinho_id INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    telemovel VARCHAR(13) UNIQUE,
    email VARCHAR(100) UNIQUE
);

DROP TABLE IF EXISTS apadrinhamentos;
CREATE TABLE apadrinhamentos (
    apadrinhamento_id INT PRIMARY KEY AUTO_INCREMENT,
    cao_id INT NOT NULL,
    padrinho_id INT NOT NULL,
    data_inicio DATE NOT NULL,
    data_fim DATE,
    CONSTRAINT fk_apadrinhamentos_caes FOREIGN KEY (cao_id) REFERENCES caes(cao_id) ON DELETE CASCADE, -- caso o cão morra ou seja adotado, faz sentido este registo ser apagado
    CONSTRAINT fk_apadrinhamentos_padrinhos FOREIGN KEY (padrinho_id) REFERENCES padrinhos(padrinho_id) ON DELETE CASCADE -- caso o padrinho queira os seus dados apagados, este registo pode desaparecer
);

DROP TABLE IF EXISTS funcionarios;
CREATE TABLE funcionarios (
    funcionario_id INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    ocupacao VARCHAR(45) NOT NULL,
    email VARCHAR(50) UNIQUE,
    telemovel VARCHAR(13) UNIQUE,
    nif VARCHAR(9) NOT NULL UNIQUE
);

DROP TABLE IF EXISTS voluntarios;
CREATE TABLE voluntarios (
    voluntario_id INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    email VARCHAR(50) UNIQUE,
    telemovel VARCHAR(13) UNIQUE,
    disponibilidade ENUM('Sabado: 9h-12h', 'Domingo: 9h-12h', 'Feriado: 9h-12h', 'Fim-de-Semana', 'Todos', 'Depende') NOT NULL
);

DROP TABLE IF EXISTS consultas_veterinario;
CREATE TABLE consultas_veterinario (
    consulta_id INT PRIMARY KEY AUTO_INCREMENT,
    cao_id INT,
    funcionario_id INT,
    data_consulta DATETIME NOT NULL,
    custo DECIMAL(8,2) NOT NULL,
    observacao VARCHAR(250),
    CONSTRAINT fk_consultas_veterinario_caes FOREIGN KEY (cao_id) REFERENCES caes(cao_id) ON DELETE SET NULL,   -- quero manter as consultas registadas 
    CONSTRAINT fk_consultas_veterinario_funcionarios FOREIGN KEY (funcionario_id) REFERENCES funcionarios(funcionario_id) ON DELETE SET NULL -- mesma situação
);

DROP TABLE IF EXISTS vacinas;
CREATE TABLE vacinas (
    vacina_id INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(50) NOT NULL
);

DROP TABLE IF EXISTS vacinacoes;
CREATE TABLE vacinacoes (
    vacinacao_id INT PRIMARY KEY AUTO_INCREMENT,
    cao_id INT NOT NULL,
    funcionario_id INT,
    vacina_id INT,
    data_vacinacao DATETIME NOT NULL, 
    CONSTRAINT fk_vacinacoes_caes FOREIGN KEY (cao_id) REFERENCES caes(cao_id) ON DELETE CASCADE, -- nao faz sentido manter registo de um cão depois de ou morrer ou sair (é passado para a pessoa que adota) 
    CONSTRAINT fk_vacinacoes_funcionarios FOREIGN KEY (funcionario_id) REFERENCES funcionarios(funcionario_id) ON DELETE SET NULL, -- nao quero apagar um registo se um funcionario sai do Centro
    CONSTRAINT fk_vacinacoes_vacinas FOREIGN KEY (vacina_id) REFERENCES vacinas(vacina_id) ON DELETE SET NULL -- se uma vacina deixar de "ser administrada" aos cães", quero que o registo se mantenha pelo historico de saude
);

DROP TABLE IF EXISTS ocorrencias_cao_voluntario;
CREATE TABLE ocorrencias_cao_voluntario (
    ocorrencias_cao_voluntario_id INT PRIMARY KEY AUTO_INCREMENT,
    cao_id INT,
    voluntario_id INT,
    data_ocorrencia DATETIME NOT NULL,
    descricao VARCHAR(500),
    seguro_ativado BOOLEAN NOT NULL DEFAULT TRUE,
    CONSTRAINT fk_ocorrencias_caes FOREIGN KEY (cao_id) REFERENCES caes(cao_id) ON DELETE SET NULL, -- quero manter o registo, supondo que o voluntario queira processar o CROAE lol
    CONSTRAINT fk_ocorrencias_voluntarios FOREIGN KEY (voluntario_id) REFERENCES voluntarios(voluntario_id) ON DELETE SET NULL -- mesma situação, pode deixar de ser voluntario mas processar na mesma
);

DROP TABLE IF EXISTS ocorrencias_entre_caes;
CREATE TABLE ocorrencias_entre_caes ( 
    ocorrencia_caes_id INT PRIMARY KEY AUTO_INCREMENT,
    cao_agressor INT NOT NULL,
    cao_envolvido INT,
    data_ocorrencia DATETIME NOT NULL,
    descricao VARCHAR(250) NOT NULL,
    CONSTRAINT fk_ocorrencias_caes_agressor FOREIGN KEY (cao_agressor) REFERENCES caes(cao_id) ON DELETE CASCADE, -- nao implica nada manter este registo
    CONSTRAINT fk_ocorrencias_caes_envolvido FOREIGN KEY (cao_envolvido) REFERENCES caes(cao_id) ON DELETE CASCADE -- nao implica nada manter este registo
);

DROP TABLE IF EXISTS ordens_tribunal;
CREATE TABLE ordens_tribunal (
    ordem_id INT PRIMARY KEY AUTO_INCREMENT,
    cao_id INT NOT NULL,
    observacao VARCHAR(250) NOT NULL,
    CONSTRAINT fk_ordens_tribunal_caes FOREIGN KEY (cao_id) REFERENCES caes(cao_id) ON DELETE CASCADE -- nao implica nada manter este registo
);

DROP TABLE IF EXISTS familia_adotantes;
CREATE TABLE familia_adotantes (
    familia_id INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR(50) NOT NULL,
    email VARCHAR(50) UNIQUE,
    telemovel VARCHAR(13) UNIQUE,
    data_registo DATE
);

DROP TABLE IF EXISTS adocoes;
CREATE TABLE adocoes (
    adocao_id INT PRIMARY KEY AUTO_INCREMENT,
    cao_id INT,
    familia_id INT,
    data_inicio_processo DATE NOT NULL,
    estado ENUM('Concluida', 'Cancelada', 'A decorrer') NOT NULL DEFAULT 'A decorrer',
    CONSTRAINT fk_adocoes_caes FOREIGN KEY (cao_id) REFERENCES caes(cao_id) ON DELETE SET NULL, -- pode acontecer um cão ser devolvido, e por isso quero manter o registo vivo
    CONSTRAINT fk_adocoes_familias FOREIGN KEY (familia_id) REFERENCES familia_adotantes(familia_id) ON DELETE SET NULL -- uma familia pode querer os seus registos pessoais apagados mas a adoção mantem se
);

DROP TABLE IF EXISTS doacoes;
CREATE TABLE doacoes (
    doacao_id INT PRIMARY KEY AUTO_INCREMENT,
    nome_doador VARCHAR(100) NOT NULL,
    funcionario_id INT  ,
    data_doacao DATE NOT NULL,
    tipo_doacao ENUM('Monetária', 'Bens ou Comida') NOT NULL DEFAULT 'Bens ou Comida',
    valor DECIMAL(6,2),
    descricao VARCHAR(100) NOT NULL,
    CONSTRAINT fk_doacoes_funcionarios FOREIGN KEY (funcionario_id) REFERENCES funcionarios(funcionario_id) ON DELETE SET NULL -- manter registo da doação mesmo que funcionário seja apagado
);

DROP TABLE IF EXISTS fotos;
CREATE TABLE fotos (
	foto_id INT PRIMARY KEY AUTO_INCREMENT,
    cao_id INT NOT NULL,
    foto varchar(255), 
    CONSTRAINT fk_fotos_caes FOREIGN KEY (cao_id) REFERENCES caes(cao_id) ON DELETE CASCADE -- não faz sentido guardar fotos se um cão falecer ou for adotado. Sim pode ser devolvido, mas se o registo do cão ainda existe é mais facil colocar fotos que ficar a pesar na database
);

/* 
----------------------------------------------
	    Criar Utilizadores e Permissões
-----------------------------------------------
*/

-- Criar utilizadores 
DROP USER IF EXISTS 'administrador'@'localhost';
CREATE USER 'administrador'@'localhost' IDENTIFIED BY 'root';
FLUSH PRIVILEGES;

DROP USER IF EXISTS 'funcionario'@'localhost';
CREATE USER 'funcionario'@'localhost' IDENTIFIED BY 'cao123';
FLUSH PRIVILEGES;

DROP USER IF EXISTS 'utilizador'@'localhost';
CREATE USER 'utilizador'@'localhost' IDENTIFIED BY 'user123';
FLUSH PRIVILEGES;

-- Privilégios
GRANT ALL PRIVILEGES ON croae_projeto.* TO 'administrador'@'localhost' WITH GRANT OPTION;

GRANT SELECT, INSERT, UPDATE ON croae_projeto.* TO 'funcionario'@'localhost';

GRANT SELECT ON croae_projeto.caes TO 'utilizador'@'localhost';
GRANT SELECT ON croae_projeto.racas TO 'utilizador'@'localhost';

/* 
--------------------------------------------
	       Criação dos Indices 
--------------------------------------------
*/

-- Para depois calcular pela idade dos caes 
CREATE INDEX idx_data_nascimento ON caes (data_nascimento);

-- Para as racas
CREATE INDEX idx_raca_id ON caes (raca_id);

-- Consultas
CREATE INDEX idx_consultas_cao_data ON consultas_veterinario (cao_id, data_consulta);

-- Vacinações
CREATE INDEX idx_vacinacoes_cao_data ON vacinacoes (cao_id, data_vacinacao, vacina_id);

/* 
------------------------------------
	     Inserção de Dados
------------------------------------
*/

INSERT INTO racas (
	raca
)
VALUES 
	('Labrador'), 			-- 1
    ('Pastor Alemão'), 
    ('Bulldog Francês'), 
    ('Beagle'), 
    ('Golden Retriever'),  -- 5
    ('Rafeiro Alentejano'), 
    ('Rottweiler'), 
    ('Yorkshire Terrier'), 
    ('Dálmata'), 
    ('Boxer'),  -- 10
    ('Pittbull'), 
    ('Doberman'), 
    ('Cão de Água'), 
    ('Husky'), 
    ('Chihuahua'), -- 15
    ('Rafeiro');
 
 INSERT INTO alas ( -- não coloquei 4 registos porque não se adap
	tipo
 )
 VALUES
 	('Enfermaria'),
    ('Primeira Ala'),
    ('Segunda Ala'),
    ('Isolamento');

 
 INSERT INTO boxes (
	ala_id
)
VALUES
	(2),
    (2),
    (3),
    (3),
    (4),
    (1),
    (2),
    (2),
    (3),
    (2),
    (3),
    (1);
 
INSERT INTO caes (
	raca_id, nome, data_nascimento, porte, sexo, data_entrada, cruzamento_raca, box_id, castrado, caracteristica, disponivel
)
VALUES 
	(11, 'Kyra', '2020-01-01', 'Grande', 'F', '2023-06-15 10:00:00', NULL, 1, FALSE, 'Sociável, mimada e adora biscoitos!', FALSE),
	(16, 'Jaime', '2019-11-20', 'Grande', 'M', '2024-01-10 09:30:00', 'Pitbull', 2, TRUE, 'Muito meigo com crianças e adora atenção', TRUE),
	(3, 'Bê', '2018-05-03', 'Médio', 'M', '2022-08-05 14:20:00', NULL, 3, TRUE, 'Adora crianças', FALSE),
	(16, 'Tino', '2021-02-17', 'Grande', 'M', '2024-03-01 11:15:00', NULL, 1, FALSE, 'Muito energético', TRUE),
	(16, 'Timon', '2017-12-09', 'Médio', 'M', '2021-10-10 13:45:00', 'Labrador', 4, TRUE, 'Muito obediente', TRUE), -- 5
	(16, 'Dino', '2020-08-01', 'Pequeno', 'M', '2023-02-22 15:00:00', NULL, 2, TRUE, 'Calmo e educado', FALSE),
	(16, 'Simba', '2019-04-10', 'Médio', 'M', '2022-09-14 10:30:00', NULL, 3, FALSE, 'Muito curioso', TRUE),
	(16, 'Martinha', '2022-01-28', 'Médio', 'F', '2024-06-02 12:00:00', 'Pitbull', 1, FALSE, 'Adora brincar', TRUE),
	(16, 'Martinho', '2020-07-07', 'Médio', 'M', '2023-05-20 09:00:00', 'Pitbull', 4, TRUE, 'Muito dócil', TRUE),
	(11, 'Nina', '2016-06-15', 'Grande', 'F', '2020-11-30 08:15:00', NULL, 2, TRUE, 'Veterana calma e experiente', TRUE), -- 10
	(16, 'Dina', '2021-10-25', 'Pequeno', 'F', '2024-04-18 10:45:00', 'Pinscher', 3, FALSE, 'Muito ativa', TRUE),
	(16, 'Judy', '2018-01-30', 'Grande', 'F', '2021-01-01 11:00:00', NULL, 1, TRUE, 'Guardiã atenta', TRUE),
	(16, 'Pirulito', '2019-09-14', 'Médio', 'M', '2022-12-12 14:30:00', 'Pitbull', 2, FALSE, 'Muito afetuoso', TRUE),
	(16, 'Pepa', '2020-06-01', 'Pequeno', 'F', '2023-07-07 10:10:00', NULL, 4, TRUE, 'Adora correr', TRUE),
	(6, 'Joca', '2022-03-03', 'Grande', 'M', '2024-02-05 09:20:00', NULL, 3, FALSE, 'Muito brincalhão', TRUE), -- 15
	(16, 'Jynx', '2021-04-16', 'Grande', 'M', '2024-05-15 13:35:00', NULL, 1, FALSE, 'Muito inteligente', FALSE),
    (16, 'Bitoke', '2020-04-16', 'Médio', 'M', '2024-05-15 13:35:00', 'Rotweiller', 5, TRUE, 'Muito inteligente, resmungão', TRUE),
    (15, 'Chibi', '2018-01-11', 'Pequeno', 'M', '2025-04-15 13:35:00', NULL, 10, FALSE, 'Medroso e pouco sociável', FALSE),
    (16, 'Chucky', '2023-01-01', 'Grande', 'M', '2024-02-15 13:35:00', NULL, 5, FALSE, 'Adora brincar na Praia', TRUE),
    (16, 'Duque', '2022-08-15', 'Grande', 'M', '2023-03-24 13:35:00', NULL, 1, TRUE, 'Adora crianças e festas', TRUE), -- 20
    (16, 'Faial', '2023-01-01', 'Grande', 'M', '2024-03-24 13:35:00', 'Pitbull', 1, TRUE, 'Adora correr e jogar com uma bola', TRUE),
    (6, 'Jeny', '2023-05-05', 'Grande', 'F', '2024-03-24 13:35:00', NULL, 1, TRUE, 'Passeios grandes mas demorados', TRUE),
    (16, 'Jujú', '2023-05-05', 'Pequeno', 'F', '2025-07-01 13:35:00', NULL, 1, TRUE, 'Adora Mimos', TRUE),
    (16, 'Migalha', '2025-01-01', 'Pequeno', 'M', '2025-06-24 13:35:00', NULL, 1, TRUE, 'Medroso', TRUE); -- 24
    
    
INSERT INTO padrinhos ( 
	nome, email, telemovel
)
VALUES
	('António Simões', 'antoniosimoes@gmail.com', '944444444'),
	('Beto', 'beto@gmail.com', '944444445'),
	('Carlos Mozer', 'mozer@gmail.com', '944444446'),
	('Di Maria', 'dimagia@gmail.com', '944444447'),
	('Enzo Pérez', 'enzo@gmail.com', '944444448'),
	('Fabrizio Micoli', 'miccoli@gmail.com', '944444449'),
	('Gaitán', 'gaitan@gmail.com', '944444450'),
	('Hélder Cristóvão', 'cristovao@gmail.com', '944444451'),
	('Isaías', 'isaias@gmail.com', '944444453'),
	('Jota', 'celtic@gmail.com', '944444454');
  
INSERT INTO apadrinhamentos (
	cao_id, padrinho_id, data_inicio, data_fim
)
VALUES
	(1, 1, '2025-01-10', '2025-06-10'),
	(2, 2, '2025-03-01', NULL),
	(3, 3, '2022-11-20', '2025-05-20'),
	(4, 4, '2024-01-15', NULL),
	(4, 5, '2023-07-01', '2025-01-01'),
	(5, 5, '2024-05-10', NULL),
	(7, 5, '2022-09-05', '2025-02-05'),
	(8, 6, '2024-03-25', NULL),
	(9, 5, '2023-12-12', '2025-06-12'),
	(10, 10, '2025-01-01', NULL);

INSERT INTO funcionarios ( 
	nome, ocupacao, email, telemovel, nif
)
VALUES  -- São todos nomes ficticios com datas ficticias
	('Kostas Mitroglou', 'Tratador', 'mitrogolo@gmail.com', '911111111', '111111111'),
	('Luisão', 'Veterinário', 'luisilva@gmail.com', '911111112', '222222222'),
	('Mantorras', 'Veterinário', 'mantorras@gmail.com', '911111113', '333333333'),
	('Nuno Gomes', 'Tratador', 'nunogomes@gmail.com', '911111114', '444444444'),
	('Óscar Cardozo', 'Tratador', 'tenhamcuidado@gmail.com', '911111115', '555555555'),
	('Pablo Aimar', 'Admin', 'mago@gmail.com', '911111116', '666666666'),
	('Quim', 'Tratador', 'quim@gmail.com', '911111117', '777777777'),
	('Rollheiser', 'Auxiliar', 'roll@gmail.com', '911111118', '888888888'),
	('Salvio', 'Treinador', 'salvio@gmail.com', '911111119', '999999999'),
	('Talisca', 'Admin', 'talisca@gmail.com', '911111120', '000000000');

INSERT INTO voluntarios (
	nome, email, telemovel, disponibilidade
) 
VALUES -- São todos dados ficticios com datas ficticias
('Abel Xavier', 'cabelo@gmail.com', '921111111', 'Sabado: 9h-12h'),
('Bernardo Silva', 'bernardosilva@gmail.com', '921111112', 'Domingo: 9h-12h'),
('Chalana','chalana@gmail.com', '921111113', 'Feriado: 9h-12h'),
('David Luiz', 'davidluiz@gmail.com', '921111114', 'Fim-de-Semana'),
('Enzo Fernandez', 'falso@gmail.com', '921111115', 'Todos'),
('Filipa Patão', 'filipapato@gmail.com', '921111116', 'Sabado: 9h-12h'),
('Garay', 'garay@gmail.com', '921111117', 'Domingo: 9h-12h'),
('Hugo Félix', 'irmaomenosconhecido@gmail.com', '921111118', 'Depende'),
('Ivan Cavaleiro','ivan@gmail.com', '921111119', 'Fim-de-Semana'),
('João Félix', 'irmaomaisconhecido@gmail.com', '921111120', 'Todos');

INSERT INTO consultas_veterinario (
	cao_id, funcionario_id, data_consulta, custo, observacao
)
VALUES
	(1, 2, '2018-07-01 10:30:00', 135.00, 'Análises Semestrais'),
	(2, 3, '2019-07-02 14:00:00', 50.00, 'Consulta de urgência: vómitos e diarreia. Tratado com medicação.'),
	(3, 1, '2020-07-03 09:15:00', 45.00, 'Check-up geral, foi recomendado ração diferente'),
	(4, 4, '2025-07-04 11:45:00', 60.00, 'Limpeza de tártaro'),
	(4, 2, '2025-07-05 13:30:00', 30.00, 'Revisão pós-vacinação. Tudo normal.'),
    (5, 2, '2025-07-01 10:30:00', 135.00, 'Análises Semestrais'),
	(6, 3, '2025-07-02 14:00:00', 500.00, 'Consulta de urgência: vomitar sangue. Ficou hospitalizado'),
	(7, 1, '2025-07-03 09:15:00', 45.00, 'Check-up geral'),
	(8, 4, '2025-07-04 11:45:00', 65.00, 'Extração dentária. Recuperação prevista em 3 dias.'),
	(2, 2, '2025-07-05 13:30:00', 135.00, 'Castração');

INSERT INTO vacinas (
	nome
)
VALUES
	('Esgana Canina'),
    ('Hepatite Infeciosa Canina'),
    ('Parvovirose'),
    ('Leptospirose'),
    ('Raiva'),
    ('Tosse do Canil'),
    ('Doença de Lyme'),
    ('Babesiose'),
    ('Infeção por fungos'),
    ('Leishmaniose'),
    ('Outro');

INSERT INTO vacinacoes (
	cao_id, funcionario_id, vacina_id, data_vacinacao
)
VALUES
	(1, 2, 1, '2019-06-10'),
	(2, 3, 2, '2020-07-01'),
	(3, 1, 3, '2021-06-20'),
	(4, 4, 1, '2022-07-03'),
	(2, 2, 4, '2023-07-05'),
    (5, 2, 1, '2025-06-10'),
	(6, 3, 2, '2025-07-01'),
	(7, 1, 3, '2025-06-20'),
	(8, 4, 1, '2025-07-03'),
	(9, 2, 4, '2025-07-05');

INSERT INTO ocorrencias_cao_voluntario (
	cao_id, voluntario_id, data_ocorrencia, descricao, seguro_ativado
)
VALUES 
	(1, 2, '2019-06-10 14:30:00', 'Cão mordeu ligeiramente orelha de voluntário durante a retirada da BOX. Sem ferimentos', FALSE),
	(2, 3, '2025-06-12 10:00:00', 'Voluntário caiu enquanto segurava o cão na trela. Sem ferimentos.', FALSE),
	(3, 1, '2025-06-15 16:45:00', 'Cão fugiu durante o passeio. Foi recuperado.', FALSE),
	(4, 4, '2025-07-01 09:10:00', 'Voluntário mordido por cão quando se preparava para tirar a trela. Havia comida na box.', TRUE),
	(1, 2, '2025-07-03 11:20:00', 'Interação normal, mas cão recusou sair do canil. Nada grave.', FALSE),
	(4, 1, '2025-07-03 16:45:00', 'Cão fugiu durante o passeio. Foi recuperado.', FALSE),
	(3, 1, '2025-06-15 16:45:00', 'Cão mordeu voluntário quando se preparava para sair da box', TRUE),
	(3, 1, '2025-06-15 16:45:00', 'Voluntário entrou em box com aviso para não entrar, foi atacado.', TRUE),
	(3, 1, '2025-06-15 16:45:00', 'Cão recusou colocar a trela', FALSE),
	(3, 1, '2025-06-15 16:45:00', 'Cão fugiu durante passeio, e atacou um cidadão', TRUE);

INSERT INTO ocorrencias_entre_caes (
	cao_agressor, cao_envolvido, data_ocorrencia, descricao
)
VALUES
	(1, 3, '2025-06-11 13:15:00', 'Cão 1 atacou o cão 3 durante o tempo de recreio. Sem ferimentos.'),
	(2, 4, '2025-06-14 15:30:00', 'Disputa por comida entre caes na Box. Foram separados por agora.'),
	(3, 2, '2025-06-20 17:05:00', 'Cão 3 mordeu o pescoço do cão 2. Cão 2 foi levado de urgência para Hospital'),
	(4, 1, '2025-07-01 08:50:00', 'Cão 4 mordeu cão 1 na Box. Foram separados por agora.'),
	(2, 1, '2017-07-04 12:00:00', 'Disputa agressiva durante alimentação conjunta. Sem ferimentos, mas foram separados.'),
    (1, 3, '2019-06-11 13:15:00', 'Durante o passeio, cão 1 soltou-se e atacou o cão 3.'),
	(1, 4, '2018-06-11 13:15:00', 'Durante o passeio, cão 1 soltou-se e atacou o cão 4.'),
	(3, 2, '2025-06-20 17:05:00', 'Cão 3 mordeu entre as grades cão 2 que estava a voltar à box. Foi colocado na enfermaria'),
	(2, 1, '2025-07-04 12:00:00', 'Reação agressiva durante alimentação conjunta. Isolamento temporário aplicado.');

INSERT INTO ocorrencias_entre_caes (
	cao_agressor, data_ocorrencia, descricao
)
VALUES 
	(4, '2025-06-11 13:15:00', 'Cão 4 fez ferida no nariz por estar sempre a colocar o focinho debaixo da porta');

INSERT INTO ordens_tribunal ( -- devia ter campo data 
	cao_id, observacao
)
VALUES
	(1, 'Cão recolhido por ordem judicial devido a maus-tratos.'),
	(2, 'Tribunal ordenou reabilitação obrigatória antes da adoção.'),
	(3, 'Animal retirado de residência por negligência grave.'),
	(4, 'Proibição de contacto com o animal imposta ao anterior detentor.'),
	(1, 'Restrição de devolução ao anterior tutor decretada.'),
    (5, 'Medida cautelar de afastamento imediato do ambiente de risco.'),
	(6, 'Adoção condicionada a relatório positivo de reintegração comportamental.'),
	(7, 'Aplicação de coima e restrição de adoção futura decretadas.'),
	(8, 'Ordem de acompanhamento veterinário regular imposta pelo tribunal.'),
	(9, 'Encaminhamento do animal para programa de reabilitação comportamental judicialmente supervisionado.');
    
INSERT INTO familia_adotantes (
	nome, email, telemovel
)
VALUES
	('Costa Pereira', 'costapereira@gmail.com', '933333331'),
	('Mário João', 'mariojoao@gmail.com', '933333332'),
	('Germano de Figueiredo', 'germano@gmail.com', '933333333'),
	('Ângelo Martins', 'angelomartins@gmail.com', '933333334'),
	('José Neto', 'joseneto@gmail.com', '933333335'),
    ('Cruz', 'cruz@gmail.com', '933333336'),
	('José Augusto', 'joseaugusto@gmail.com', '933333337'),
	('Joaquim Santana', 'joaquimsantana@gmail.com', '933333338'),
	('José Águas', 'joseaguas@gmail.com', '933333339'),
	('Mário Coluna', 'mariocoluna@gmail.com', '933333340'),
    ('Domiciano Cavém', 'domicianocavem@gmail.com', '933333341');
    
INSERT INTO adocoes (
	cao_id, familia_id, data_inicio_processo, estado
)
VALUES
	(1, 2, '2025-06-01', 'Concluida'),
	(2, 3, '2025-06-10', 'Cancelada'),
	(3, 1, '2025-06-15', 'A decorrer'),
	(4, 4, '2025-07-01', 'Concluida'),
	(5, 5, '2025-07-05', 'A decorrer'),
    (6, 2, '2025-06-01', 'Concluida'),
	(7, 3, '2025-06-10', 'Cancelada'),
	(8, 1, '2025-06-15', 'A decorrer'),
	(9, 4, '2025-07-01', 'Concluida'),
	(10, 5, '2025-07-05', 'A decorrer');

INSERT INTO doacoes (
	nome_doador, funcionario_id, data_doacao, tipo_doacao, valor, descricao
)
VALUES -- fiquei sem nomes para jogadores
	('Pogacar', 1, '2025-06-05', 'Monetária', 100.00, 'Doação para cobrir despesas veterinárias.'),
	('Roglic', 2, '2025-06-08', 'Bens ou Comida', 0.00, 'Entregou 10kg de ração e cobertores.'),
	('João Almeida', 1, '2025-06-15', 'Monetária', 50.00, 'Ajuda mensal ao centro.'),
	('Julian Alaphillippe', 3, '2025-06-20', 'Bens ou Comida', 0.00, 'Doou produtos de higiene animal.'),
	('Van der Poel', 4, '2025-07-01', 'Monetária', 200.00, 'Doação em memória do seu cão adotado.'),
    ('Carapaz', 1, '2025-06-05', 'Monetária', 100.00, 'Doação para cobrir despesas veterinárias.'),
	('Joaquim Agostinho', 2, '2025-06-08', 'Bens ou Comida', 0.00, 'Entregou 10kg de ração e cobertores.'),
	('Rui Oliveira', 1, '2025-06-15', 'Monetária', 50.00, 'Ajuda mensal ao abrigo.'),
	('Continente', 3, '2025-06-20', 'Bens ou Comida', 0.00, 'Doou produtos de higiene animal, ração e cobertores recolhidos.'),
	('Afonso Eulálio', 4, '2025-07-01', 'Monetária', 200.00, 'Doação em memória do seu cão adotado.');

INSERT INTO fotos (cao_id, foto)
VALUES
(1, 'images/adotados/kyra.jpg'),
(2, 'images/adotados/jaime.jpg'),
(3, 'images/adotados/be.jpg'),
(20, 'images/adotados/duque.jpg'),
(8, 'images/adotados/martinha.jpg'),
(6, 'images/adotados/dino.jpg'),
(7, 'images/adotados/simba.jpg'),
(9, 'images/adotados/martinho.jpg'),
(10, 'images/adotados/nina.jpg'),
(11, 'images/adotados/dina.jpg'),
(12, 'images/adotados/judy.jpg'),
(13, 'images/adotados/pirulito.jpg'),
(14, 'images/adotados/pepa.jpg'),
(15, 'images/adotados/joca.jpg'),
(16, 'images/adotados/jynx.jpg'),
(17, 'images/adotados/bitoke.jpg'),
(18, 'images/adotados/chibi.jpg'),
(19, 'images/adotados/chucky.jpg'),
(21, 'images/adotados/faial.jpg'),
(22, 'images/adotados/jeny.jpg'),
(23, 'images/adotados/juju.jpg'),
(5, 'images/adotados/timon.jpg'),
(4, 'images/adotados/tino.jpg'),
(24, 'images/adotados/migalha.jpg');


