# Voluntariado no CROAE da Moita

Tal como para a UC00604, todo este trabalho centrou-se à volta dos animais do CROAE. Mostrar a realidade do que é o CROAE - Centro de Recolha de Animais da Moita - e demonstrar a necessidade de um website para facilitar aos utilizadores a adoção responsável.

## Tecnologias Utilizadas

- Frotend desenvolvido com HTML, CSS (Bootstrap 5.0) e Javascript;
- Base de dados em MYSQL;
  - Utilização de Pomelo MySQL Scaffolding para ler a base de dados existente e gerar todas as classes necessárias automáticas em C#.
- Criação de API's através de C#, em que javascript utiliza fetch _address_ para connectar à base através dos endpoints criados, e C# consume estes dados para colocar na base de dados (caso sejam válidos).
- Utilização de Cache, através de duas layers: Polly e Cache para evitar várias chamadas
- Autênticação através de JWT, com criação de um token para garantir que todos os dados que entram na bd são autenticados.
- Utilização de HTTPS para garantir uma maior segurança das ligações;

## Correr o Projeto Localmente

Antes de mais, fazer clone do repositório para a vossa máquina

```bash
   git clone https://github.com/Brekey53/Fullstack_Website_Adopt_A_Pet.git
```

1. Abram a pasta onde se localiza o projeto, e utilizem o browser para abrir o ficheiro `index.html`
   Caso tenham VSCode instalado, recomendo a extensão LivePreview para conseguir ver as alterações no browser _in real time_ e assim podem abrir o frontend dessa forma.

1. Garantir que têm docker instalado. Abrir o CMD (ou terminal) na pasta onde se encontra o ficheiro "docker-compose.yml" e correr o comando: _docker compose up -d_. Isto vai criar uma ligação no docker com as portas necessárias para correr o programa: a da base de dados, a do Redis e a do imposters.

1. Garantir que têm MYSQL instalado, criar uma conexão nova com a mesma porta como a da criada no docker e correr o script "Criação do CROAE.sql" para gerar a base de dados.

1. Ter Visual Studio (2022 ou Insiders 2026) instalado. Abrir a pasta API no VS e abrir mesmo o projeto. Executar em HTTPS.

1. Com o Frontend a correr em Live Preview, o Backend a correr no VS e o docker a correr com as portas, é possível testarem o projeto.

## Organização de Diretorias e Ficheiros

Para conseguir fazer esta árvore foi utilizada a extensão do VSCode chamada
`project-tree`. Instalem através das extensões do VSC e podem depois:

1. Em ambiente Windows ou Linux, `Ctrl + Shift + P` e escrever _Project Tree_. Vai gerar automaticamente a estrutura do projecto no ficheiro ReadME que tenham criado.

2. Em ambiente MAC, `⌘ + ⇧ + P` e escrever _Project Tree_.

```bash
Fullstack_Website_Adopt_A_Pet
├─ Api
│  ├─ Api
│  │  ├─ Api.http
│  │  ├─ ApiAndreLeonorProjetoFinal.csproj
│  │  ├─ appsettings.json
│  │  ├─ Controllers
│  │  │  ├─ AdotadosController.cs
│  │  │  ├─ CaesController.cs
│  │  │  ├─ DoacoesController.cs
│  │  │  ├─ FormularioContactoController.cs
│  │  │  ├─ LoginController.cs
│  │  │  ├─ PagamentosController.cs
│  │  │  ├─ PorAdotarController.cs
│  │  │  └─ RacasController.cs
│  │  ├─ Data
│  │  │  └─ CroaeDbContext.cs
│  │  ├─ Models
│  │  │  ├─ Adocoes.cs
│  │  │  ├─ Ala.cs
│  │  │  ├─ Apadrinhamento.cs
│  │  │  ├─ Box.cs
│  │  │  ├─ Caes.cs
│  │  │  ├─ CaoDto.cs
│  │  │  ├─ ConsultasVeterinario.cs
│  │  │  ├─ DoacaoDTO.cs
│  │  │  ├─ Doacoes.cs
│  │  │  ├─ FamiliaAdotante.cs
│  │  │  ├─ FormularioContacto.cs
│  │  │  ├─ Foto.cs
│  │  │  ├─ Funcionario.cs
│  │  │  ├─ Login.cs
│  │  │  ├─ OcorrenciasCaoVoluntario.cs
│  │  │  ├─ OcorrenciasEntreCaes.cs
│  │  │  ├─ OrdensTribunal.cs
│  │  │  ├─ Padrinho.cs
│  │  │  ├─ Raca.cs
│  │  │  ├─ Vacina.cs
│  │  │  ├─ Vacinacao.cs
│  │  │  └─ Voluntario.cs
│  │  ├─ Program.cs
│  │  ├─ Properties
│  │  │  └─ launchSettings.json
│  │  └─ wwwroot
│  │     └─ images
│  │        └─ adotados
│  │           ├─ be.jpg
│  │           ├─ bitoke.jpg
│  │           ├─ chibi.jpg
│  │           ├─ chucky.jpg
│  │           ├─ default.jpg
│  │           ├─ dina.jpg
│  │           ├─ dino.jpg
│  │           ├─ duque.jpg
│  │           ├─ faial.jpg
│  │           ├─ jaime.jpg
│  │           ├─ jaime_2.jpg
│  │           ├─ jeny.jpg
│  │           ├─ joca.jpg
│  │           ├─ judy.jpg
│  │           ├─ juju.jpg
│  │           ├─ kyra.jpg
│  │           ├─ martinha.jpg
│  │           ├─ martinho.jpg
│  │           ├─ migalha.jpg
│  │           ├─ nina.jpg
│  │           ├─ pepa.jpg
│  │           ├─ pirulito.jpg
│  │           ├─ simba.jpg
│  │           ├─ timon.jpg
│  │           └─ tino.jpg
│  └─ ApiAndreLeonorProjetoFinal.sln
├─ docker-compose.yml
├─ Mocks
│  └─ imposters.json
├─ README.md
├─ Script criação do CROAE.sql
└─ Website
   ├─ images
   │  ├─ croae_logo.jpg
   │  ├─ croae_logo_mobile.jpg
   │  ├─ icone_titulo.png
   │  ├─ manta.jpg
   │  ├─ osso.jpg
   │  ├─ pirulito_homepage.jpg
   │  └─ voluntario.jpg
   ├─ index.html
   ├─ js
   │  ├─ adicionarCao.js
   │  ├─ adotadosDetalhes.js
   │  ├─ carregarCaesAdotados.js
   │  ├─ carregarCaesParaAdotar.js
   │  ├─ doacoes.js
   │  ├─ editarCaes.js
   │  ├─ enviarEmail.js
   │  ├─ jquery-1.11.0.min.js
   │  ├─ lightbox.min.js
   │  ├─ login.js
   │  └─ script.js
   ├─ pages
   │  ├─ adoptionPages
   │  │  ├─ adicionarCao.html
   │  │  ├─ adocao.html
   │  │  ├─ adotados
   │  │  │  ├─ adotadosCaes.css
   │  │  │  └─ adotadosDetalhes.html
   │  │  ├─ adotados.html
   │  │  ├─ editarCaes.html
   │  │  ├─ images
   │  │  │  └─ adocao
   │  │  │     └─ croa_localizacao.jpg
   │  │  ├─ paraAdotar.html
   │  │  ├─ porAdotar
   │  │  │  └─ porAdotarCaes.css
   │  │  └─ styles_adocao
   │  │     └─ adocao.css
   │  ├─ blog
   │  │  ├─ images
   │  │  │  ├─ abandono_e_crime.jpg
   │  │  │  ├─ bolas.png
   │  │  │  ├─ campanha_adocao.png
   │  │  │  ├─ nina.png
   │  │  │  ├─ noticias_cheque_veterinario.jpg
   │  │  │  ├─ noticias_ondas_calor.jpg
   │  │  │  ├─ openday.png
   │  │  │  ├─ pepa.png
   │  │  │  └─ voluntario_por_um_dia.jpg
   │  │  ├─ noticias.html
   │  │  └─ styles
   │  │     └─ styles_noticias.css
   │  ├─ contacts
   │  │  ├─ contactos.html
   │  │  ├─ images
   │  │  │  └─ hero_photo_croae.jpg
   │  │  └─ styles
   │  │     └─ styles_contactos.css
   │  ├─ doarPagina
   │  │  └─ doacao.html
   │  ├─ login
   │  │  ├─ login.html
   │  │  └─ styles
   │  │     └─ style_login.css
   │  ├─ servicos
   │  │  ├─ images
   │  │  │  ├─ campanha_cheque_veterinario.jpg
   │  │  │  ├─ campanha_esterializacao.png
   │  │  │  ├─ campanha_microchip.jpg
   │  │  │  ├─ campanha_recolha.png
   │  │  │  ├─ campanha_recolha_alimentos.jpg
   │  │  │  └─ croae_instituicao.jpg
   │  │  ├─ servicos.html
   │  │  ├─ servicosRecolha.html
   │  │  ├─ servicosVet.html
   │  │  └─ styles
   │  │     ├─ styles_servicoRecolha.css
   │  │     ├─ styles_servicos.css
   │  │     └─ styles_servicosVet.css
   │  └─ visits
   │     ├─ images
   │     │  ├─ animais_croae.png
   │     │  └─ instalacoes_croae.jpg
   │     ├─ styles
   │     │  └─ styles_visitar.css
   │     └─ visitar.html
   └─ styles_gerais
      ├─ buttons.css
      ├─ font_awesome.css
      ├─ footer.css
      ├─ hero.css
      ├─ navbar.css
      ├─ styles_index.css
      └─ tipografia.css

Fullstack_Website_Adopt_A_Pet
├─ Api
│  ├─ Api
│  │  ├─ Api.http
│  │  ├─ ApiAndreLeonorProjetoFinal.csproj
│  │  ├─ appsettings.json
│  │  ├─ Controllers
│  │  │  ├─ AdotadosController.cs
│  │  │  ├─ CaesController.cs
│  │  │  ├─ DoacoesController.cs
│  │  │  ├─ FormularioContactoController.cs
│  │  │  ├─ LoginController.cs
│  │  │  ├─ PagamentosController.cs
│  │  │  ├─ PorAdotarController.cs
│  │  │  └─ RacasController.cs
│  │  │
│  │  ├─ Data
│  │  │  └─ CroaeDbContext.cs
│  │  │
│  │  ├─ Models
│  │  │  ├─ Adocoes.cs
│  │  │  ├─ Ala.cs
│  │  │  ├─ Apadrinhamento.cs
│  │  │  ├─ Box.cs
│  │  │  ├─ Caes.cs
│  │  │  ├─ CaoDto.cs
│  │  │  ├─ ConsultasVeterinario.cs
│  │  │  ├─ DoacaoDTO.cs
│  │  │  ├─ Doacoes.cs
│  │  │  ├─ FamiliaAdotante.cs
│  │  │  ├─ FormularioContacto.cs
│  │  │  ├─ Foto.cs
│  │  │  ├─ Funcionario.cs
│  │  │  ├─ Login.cs
│  │  │  ├─ OcorrenciasCaoVoluntario.cs
│  │  │  ├─ OcorrenciasEntreCaes.cs
│  │  │  ├─ OrdensTribunal.cs
│  │  │  ├─ Padrinho.cs
│  │  │  ├─ Raca.cs
│  │  │  ├─ Vacina.cs
│  │  │  ├─ Vacinacao.cs
│  │  │  └─ Voluntario.cs
│  │  │
│  │  ├─ Program.cs
│  │  │
│  │  ├─ Properties
│  │  │  └─ launchSettings.json
│  │  │
│  │  └─ wwwroot
│  │     └─ images
│  │        └─ adotados
│  │           ├─ be.jpg / bitoke.jpg / chibi.jpg / chucky.jpg
│  │           ├─ default.jpg / dina.jpg / dino.jpg / duque.jpg
│  │           ├─ faial.jpg / jaime.jpg / jaime_2.jpg / jeny.jpg
│  │           ├─ joca.jpg / judy.jpg / juju.jpg / kyra.jpg
│  │           ├─ martinha.jpg / martinho.jpg / migalha.jpg
│  │           ├─ nina.jpg / pepa.jpg / pirulito.jpg
│  │           └─ simba.jpg / timon.jpg / tino.jpg
│  │
│  └─ ApiAndreLeonorProjetoFinal.sln
├─ docker-compose.yml
├─ LICENSE
├─ Mocks
│  └─ imposters.json
│
├─ README.md
├─ Script criação do CROAE.sql
└─ Website
   ├─ images
   │  ├─ croae_logo.jpg
   │  ├─ croae_logo_mobile.jpg
   │  ├─ icone_titulo.png
   │  ├─ manta.jpg
   │  ├─ osso.jpg
   │  ├─ pirulito_homepage.jpg
   │  └─ voluntario.jpg
   │
   ├─ index.html
   ├─ js
   │  ├─ adicionarCao.js
   │  ├─ adotadosDetalhes.js
   │  ├─ carregarCaesAdotados.js
   │  ├─ carregarCaesParaAdotar.js
   │  ├─ doacoes.js
   │  ├─ editarCaes.js
   │  ├─ enviarEmail.js
   │  ├─ jquery-1.11.0.min.js
   │  ├─ lightbox.min.js
   │  ├─ login.js
   │  └─ script.js
   │
   ├─ pages
   │  ├─ adoptionPages
   │  │  ├─ adicionarCao.html
   │  │  ├─ adocao.html
   │  │  ├─ adotados
   │  │  │  ├─ adotadosCaes.css
   │  │  │  └─ adotadosDetalhes.html
   │  │  │
   │  │  ├─ adotados.html
   │  │  ├─ editarCaes.html
   │  │  ├─ images
   │  │  │  └─ adocao
   │  │  │     └─ croa_localizacao.jpg
   │  │  │
   │  │  ├─ paraAdotar.html
   │  │  ├─ porAdotar
   │  │  │  └─ porAdotarCaes.css
   │  │  │
   │  │  └─ styles_adocao
   │  │     └─ adocao.css
   │  │
   │  ├─ blog
   │  │  ├─ images
   │  │  │  ├─ abandono_e_crime.jpg
   │  │  │  ├─ bolas.png
   │  │  │  ├─ campanha_adocao.png
   │  │  │  ├─ nina.png
   │  │  │  ├─ noticias_cheque_veterinario.jpg
   │  │  │  ├─ noticias_ondas_calor.jpg
   │  │  │  ├─ openday.png
   │  │  │  ├─ pepa.png
   │  │  │  └─ voluntario_por_um_dia.jpg
   │  │  │
   │  │  ├─ noticias.html
   │  │  └─ styles
   │  │     └─ styles_noticias.css
   │  │
   │  ├─ contacts
   │  │  ├─ contactos.html
   │  │  ├─ images
   │  │  │  └─ hero_photo_croae.jpg
   │  │  │
   │  │  └─ styles
   │  │     └─ styles_contactos.css
   │  │
   │  ├─ doarPagina
   │  │  └─ doacao.html
   │  │
   │  ├─ login
   │  │  ├─ login.html
   │  │  └─ styles
   │  │     └─ style_login.css
   │  │
   │  ├─ servicos
   │  │  ├─ images
   │  │  │  ├─ campanha_cheque_veterinario.jpg
   │  │  │  ├─ campanha_esterializacao.png
   │  │  │  ├─ campanha_microchip.jpg
   │  │  │  ├─ campanha_recolha.png
   │  │  │  ├─ campanha_recolha_alimentos.jpg
   │  │  │  └─ croae_instituicao.jpg
   │  │  │
   │  │  ├─ servicos.html
   │  │  ├─ servicosRecolha.html
   │  │  ├─ servicosVet.html
   │  │  └─ styles
   │  │     ├─ styles_servicoRecolha.css
   │  │     ├─ styles_servicos.css
   │  │     └─ styles_servicosVet.css
   │  │
   │  └─ visits
   │     ├─ images
   │     │  ├─ animais_croae.png
   │     │  └─ instalacoes_croae.jpg
   │     │
   │     ├─ styles
   │     │  └─ styles_visitar.css
   │     └─ visitar.html
   │
   └─ styles_gerais
      ├─ buttons.css
      ├─ font_awesome.css
      ├─ footer.css
      ├─ hero.css
      ├─ navbar.css
      ├─ styles_index.css
      └─ tipografia.css

Fullstack_Website_Adopt_A_Pet/
├─ Api/                              # Backend da aplicação (.NET Core)
│  ├─ Api/                           # Projeto principal da API
│  │  ├─ Api.http                    # Ficheiro de teste de endpoints HTTP (VS/VS Code)
│  │  ├─ ApiAndreLeonorProjetoFinal.csproj # Ficheiro de configuração do projeto .NET
│  │  ├─ appsettings.json            # Configurações globais (ex: Connection Strings da BD)
│  │  ├─ Controllers/                # Controladores: Definem os endpoints da API
│  │  │  ├─ AdotadosController.cs    # Gere as operações relacionadas com animais adotados
│  │  │  ├─ CaesController.cs        # CRUD principal dos cães
│  │  │  ├─ DoacoesController.cs     # Gere registos de doações
│  │  │  ├─ FormularioContactoController.cs # Processa envios do formulário de contacto
│  │  │  ├─ LoginController.cs       # Gere autenticação e login
│  │  │  ├─ PagamentosController.cs  # Processamento de pagamentos/donativos
│  │  │  ├─ PorAdotarController.cs   # Gere animais disponíveis para adoção
│  │  │  └─ RacasController.cs       # Gere a lista de raças disponíveis
│  │  │
│  │  ├─ Data/                       # Camada de dados
│  │  │  └─ CroaeDbContext.cs        # Contexto da Base de Dados (Entity Framework)
│  │  │
│  │  ├─ Models/                     # Modelos: Representam as tabelas da Base de Dados
│  │  │  ├─ Adocoes.cs, Caes.cs...   # Entidades principais (Cães, Adoções, etc.)
│  │  │  ├─ Login.cs, Funcionario.cs # Entidades de gestão de utilizadores
│  │  │  └─ ...                      # (Outros modelos de dados e DTOs)
│  │  │
│  │  ├─ Program.cs                  # Ponto de entrada e configuração dos serviços da API
│  │  │
│  │  ├─ Properties/
│  │  │  └─ launchSettings.json      # Configurações de execução local (portas, perfis)
│  │  │
│  │  └─ wwwroot/                    # Ficheiros estáticos servidos pela API
│  │     └─ images/
│  │        └─ adotados/             # Repositório de imagens dos animais carregadas
│  │           │                     # Fotos individuais dos cães
│  │           ├─ be.jpg / bitoke.jpg / chibi.jpg / chucky.jpg
│  │           ├─ default.jpg / dina.jpg / dino.jpg / duque.jpg
│  │           ├─ faial.jpg / jaime.jpg / jaime_2.jpg / jeny.jpg
│  │           ├─ joca.jpg / judy.jpg / juju.jpg / kyra.jpg
│  │           ├─ martinha.jpg / martinho.jpg / migalha.jpg
│  │           ├─ nina.jpg / pepa.jpg / pirulito.jpg
│  │           └─ simba.jpg / timon.jpg / tino.jpg
│  │
│  └─ ApiAndreLeonorProjetoFinal.sln # Solução do Visual Studio (agrega os projetos)
│
├─ docker-compose.yml                # Orquestração de contentores (API + BD)
├─ LICENSE                           # Licença do projeto
├─ Mocks/                            # Simulação de serviços externos
│  └─ imposters.json                 # Configuração para Mountebank (Mock Server)
│
├─ README.md                         # Documentação principal do projeto
├─ Script criação do CROAE.sql       # Script SQL para criar a estrutura da Base de Dados
│
└─ Website/                          # Frontend da aplicação (HTML/CSS/JS)
   ├─ images/                        # Imagens gerais do layout do site
   │  ├─ croae_logo.jpg              # Logótipo
   │  ├─ icone_titulo.png            # Favicon ou ícone
   │  └─ ...                         # Outros assets gráficos globais
   │
   ├─ index.html                     # Homepage (Página Inicial)
   ├─ js/                            # Scripts de lógica do Frontend
   │  ├─ adicionarCao.js             # Lógica para adicionar novos registos
   │  ├─ carregarCaesAdotados.js     # Consome a API para listar cães adotados
   │  ├─ carregarCaesParaAdotar.js   # Consome a API para listar cães disponíveis
   │  ├─ doacoes.js                  # Lógica da página de doações
   │  ├─ login.js                    # Autenticação e gestão de sessão
   │  ├─ script.js                   # Scripts globais (Navbar, Footer, UI)
   │  └─ ...                         # Bibliotecas (jQuery, Lightbox) e outros scripts
   │
   ├─ pages/                         # Páginas internas do site
   │  ├─ adoptionPages/              # Módulo de Adoção
   │  │  ├─ adicionarCao.html        # Formulário para inserir novo animal (Admin)
   │  │  ├─ adocao.html              # Página informativa sobre adoção
   │  │  ├─ adotados/                # Detalhes dos animais adotados
   │  │  │  ├─ adotadosCaes.css      # Estilos específicos desta secção
   │  │  │  └─ adotadosDetalhes.html # Template de detalhe do animal
   │  │  ├─ adotados.html            # Listagem de todos os adotados
   │  │  ├─ editarCaes.html          # Página de edição de registos (Admin)
   │  │  ├─ paraAdotar.html          # Listagem de animais disponíveis
   │  │  ├─ porAdotar/               # Detalhes e estilos dos animais disponíveis
   │  │  └─ styles_adocao/           # CSS específico das páginas de adoção
   │  │
   │  ├─ blog/                       # Módulo de Notícias
   │  │  ├─ noticias.html            # Listagem de notícias e eventos
   │  │  └─ styles/                  # Estilos do blog
   │  │
   │  ├─ contacts/                   # Módulo de Contactos
   │  │  ├─ contactos.html           # Página com mapa e formulário
   │  │  └─ styles/                  # Estilos da página de contactos
   │  │
   │  ├─ doarPagina/                 # Módulo de Doações
   │  │  └─ doacao.html              # Página com informação para donativos
   │  │
   │  ├─ login/                      # Módulo de Login
   │  │  ├─ login.html               # Formulário de acesso reservado
   │  │  └─ styles/                  # Estilos da página de login
   │  │
   │  ├─ servicos/                   # Módulo de Serviços (Vet, Recolha, etc.)
   │  │  ├─ servicos.html            # Menu geral de serviços
   │  │  ├─ servicosVet.html         # Página de serviços veterinários
   │  │  └─ styles/                  # CSS das páginas de serviços
   │  │
   │  └─ visits/                     # Módulo de Visitas
   │     ├─ visitar.html             # Página de agendamento/info de visitas
   │     └─ styles/                  # Estilos da página de visitas
   │
   └─ styles_gerais/                 # CSS Global (Reutilizável)
      ├─ buttons.css                 # Estilos de botões
      ├─ font_awesome.css            # Ícones
      ├─ footer.css                  # Estilo do rodapé
      ├─ hero.css                    # Estilo dos banners principais
      ├─ navbar.css                  # Estilo do menu de navegação
      ├─ styles_index.css            # CSS específico da Homepage
      └─ tipografia.css              # Definição de fontes e textos


```

## Autores

### Desenvolvido por André Correia e Leonor Joaquim || 2025

<<<<<<< HEAD
[![GitHub](https://img.shields.io/badge/GitHub-leonormcjoaquim-181717?style=for-the-badge&logo=github)](https://github.com/leonormcjoaquim) [![GitHub](https://img.shields.io/badge/GitHub-Brekey53-181717?style=for-the-badge&logo=github)](https://github.com/Brekey53)

```

```
=======
[![GitHub](https://img.shields.io/badge/GitHub-Brekey53-181717?style=for-the-badge&logo=github)](https://github.com/Brekey53) [![GitHub](https://img.shields.io/badge/GitHub-leonormcjoaquim-181717?style=for-the-badge&logo=github)](https://github.com/leonormcjoaquim)
>>>>>>> 28782b28653e7776d92098cdd4f9a4c0714ae408
