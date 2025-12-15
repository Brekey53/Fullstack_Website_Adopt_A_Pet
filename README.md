# 🐶 Voluntariado no CROAE da Moita

Desde 2024 que faço voluntariado no **CROAE** do Concelho **Moita** em que todos os fins de semana de manhã participo e auxilio no convivio e bem estar dos >60 animais que vivem atualmente no CROAE.

## ✍️ O que falta melhorar

Apesar de todo o trabalho positivo que tem vindo a ser desenvolvido pelo CROAE desde a sua inauguração, considero que existe uma **falha gigante** que é a falta de um **site disponível** para que qualquer pessoa possa conhecer o que é o CROAE e os animais que lá habitam.

Um site seria importante para:

1. Divulgar os animais disponíveis para adoção.
2. Informar sobre horários, contactos e serviços disponíveis.
3. Partilhar eventos, campanhas e necessidades do centro.

## ✨ Objetivo com este Projeto

Dar mais visibilidade ao trabalho do CROAE e contribuir para que esta falha seja colmatada no futuro (quiçá vender a ideia à Câmara Municipal da Moita).

## 💡💭 Inspirações para o Projecto

Para poder criar um site do zero que fosse direcionado essencialmente aos animais, consultei os seguintes websites como inspiração ou ideias que podia recriar para este
projecto:

1. [Centro de Recolha Oficial de Animais de Companhia de Sesimbra](https://www.sesimbra.pt/croac) - para Cores e tipografia
2. [Lisboa Pet - Animais para Adoção em Lisboa](https://www.lisboa.pet/adocao) - para a página de adoção de animais com cards e imagens
3. [CROAC - Canil e Gatil Municipal de Setúbal](https://www.mun-setubal.pt/croac-canil-gatil-municipal/#1542381591027-609779f1-3595) - para o acordeon

## 🌍 Live Preview com Github Pages

<https://leonormcjoaquim.github.io/Leonor_Joaquim_Projecto_TPSI_0525/>

## 🔗 Correr o Projeto Localmente

1. Fazer clone do repositório para a vossa máquina

```bash
   git clone https://github.com/leonormcjoaquim/Leonor_Joaquim_Projecto_TPSI_0525
```

2. Abram a pasta onde se localiza o projeto, e utilizem o browser para abrir o ficheiro `index.html`
   Caso tenham VSCode instalado, recomendo a extensão LivePreview para conseguir ver as alterações no browser _in real time_ e assim podem abrir o projecto dessa forma.

## 🚀 Features Utilizadas

- **HTML5**
- **CSS3** + **Bootstrap 5**
  - Utilização de **Accordion** e **Carousel** para uma apresentação diferente, mais interativa e apelativa ao utilizador
- **JavaScript**
  - Utilização de um botão **Back to Top**, uma **Navbar fixa mas que encolhe com scroll** e um **formulário sem backend** mas que interage com o utilizador.
- **Font Awesome** (ícones)
- **Google Fonts** (Roboto)
- **Lightbox2** (galerias de imagens em overlay) - muito obrigada a [@lokesh](https://github.com/lokesh/lightbox2) - repositório público de lightbox e de toda a documentação
- **GLightbox** (para o banner em visitar.html e conseguir fazer zoom, algo que não dá com lightbox) — desenvolvido por [@biati-digital](https://github.com/biati-digital/glightbox)

- **Site para retirar gifs usados dos cães** [gifsCães](https://www.animatedimages.org/cat-dogs-202.htm)

## 📁📄 Organização de Diretorias e Ficheiros

Para conseguir fazer esta árvore utilizei a extensão do VSCode chamada
`project-tree`. Instalem através das extensões do VSC e podem depois:

1. Em ambiente Windows ou Linux, `Ctrl + Shift + P` e escrever _Project Tree_. Vai gerar automaticamente a estrutura do projecto no ficheiro ReadME que tenham criado.Os comentários são adicionei individualmente.

2. Em ambiente MAC, `⌘ + ⇧ + P` e escrever _Project Tree_.

```bash
Leonor_Joaquim_Projecto_TPSI_0525/
├─ images/                     # Imagens gerais usadas no index.html ou navbar
│  ├─ croae_logo.jpg            # Logótipo principal (versão desktop)
│  ├─ croae_logo_mobile.jpg     # Logótipo alternativo para versão mobile
│  ├─ pirulito_homepage.jpg     # Imagem de destaque no index (ex: banner)
│  └─ voluntario.jpg            # Imagem usada na homepage ou secção de voluntariado
│
├─ index.html                   # Página principal (homepage)
│
├─ js/                          # Scripts JavaScript
│  ├─ jquery-1.11.0.min.js      # Biblioteca jQuery necessária para o Lightbox
│  ├─ lightbox.min.js           # Script da biblioteca Lightbox2 (galeria de imagens)
│  └─ script.js                 # JS personalizado (backToTop, navbar, formulários, etc.)
│
├─ pages/                       # Páginas e secções internas do site
│  │
│  ├─ adoptionPages/            # Secção de Adoção
│  │  ├─ adocao.html            # Página principal de informação sobre adoção
│  │  ├─ adotados/              # Páginas individuais de animais já adotados
│  │  │  ├─ 1.html - 6.html     # Fichas individuais dos animais adotados
│  │  │  ├─ adotadosCaes.css    # Estilos específicos para páginas de cães adotados
│  │  │  └─ images/             # Imagens dos cães adotados (versões normais e thumbnails)
│  │  │     ├─ be.jpg / be_thumb.jpg
│  │  │     ├─ dina.jpg / dina_thumb.jpg
│  │  │     ├─ dino.jpg / dino_thumb.jpg
│  │  │     ├─ duque.jpg / duque_thumb.jpg
│  │  │     ├─ martinha.jpg / martinha_thumb.jpg
│  │  │     ├─ nono.jpg / nono_thumb.jpg
│  │  │
│  │  ├─ adotados.html          # Página geral com a lista dos animais adotados
│  │  ├─ images/                # Imagens usadas nesta secção
│  │  │  ├─ adocao/             # Imagens relacionadas com o processo de adoção
│  │  │  │  └─ croa_localizacao.jpg
│  │  │  ├─ adotados/           # Imagens gerais para a secção “Adotados”
│  │  │  └─ porAdotar/          # Imagens dos animais disponíveis para adoção
│  │  │     ├─ jaime_2.jpg, kyra.png, migalha.jpg, nina.jpg, pirulito.jpg, timon.jpg
│  │  │
│  │  ├─ paraAdotar.html        # Página principal com lista de animais para adoção
│  │  ├─ porAdotar/             # Fichas individuais dos animais ainda por adotar
│  │  │  ├─ 1.html - 6.html     # Páginas individuais de cada cão disponível
│  │  │  ├─ porAdotarCaes.css   # Estilos específicos para as fichas de cães disponíveis
│  │  │  └─ images/             # Imagens e thumbnails dos animais por adotar
│  │  │     ├─ jaime.jpg / jaime_2.jpg / jaime_2_thumb.jpg
│  │  │     ├─ kyra.png / kyra_thumb.jpg
│  │  │     ├─ migalha.jpg / migalha_thumb.jpg
│  │  │     ├─ nina.jpg / nina_thumb.jpg
│  │  │     ├─ pirulito.jpg / pirulito_thumb.jpg
│  │  │     ├─ timon.jpg / timon_thumb.jpg
│  │  │
│  │  └─ styles_adocao/         # CSS especializado da secção de adoção
│  │     ├─ adocao.css          # Estilos da página principal de adoção
│  │     ├─ adotados.css        # Estilos da lista de animais adotados
│  │     └─ paraAdotar.css      # Estilos da lista de animais disponíveis
│  │
│  ├─ blog/                     # Secção de notícias e artigos
│  │  ├─ images/                # Imagens usadas nos artigos e listagem
│  │  │  ├─ abandono_e_crime.jpg, bolas.png, campanha_adocao.png, etc.
│  │  ├─ noticias.html           # Página principal da secção de notícias
│  │  └─ styles/
│  │     └─ styles_noticias.css  # CSS dedicado à secção de blog/notícias
│  │
│  ├─ contacts/                 # Página de contactos
│  │  ├─ contactos.html         # Página com formulário e informação de contacto
│  │  ├─ images/                # Imagens específicas da secção
│  │  │  └─ hero_photo_croae.jpg
│  │  └─ styles/
│  │     └─ styles_contactos.css # CSS da secção de contactos
│  │
│  ├─ servicos/                 # Secção de serviços CROAE
│  │  ├─ images/                # Imagens usadas nas páginas de serviços
│  │  │  ├─ campanha_cheque_veterinario.jpg, campanha_esterializacao.png, etc.
│  │  ├─ servicos.html          # Página geral dos serviços
│  │  ├─ servicosRecolha.html   # Página sobre serviços de recolha
│  │  ├─ servicosVet.html       # Página sobre serviços veterinários
│  │  └─ styles/                # Estilos dedicados à secção de serviços
│  │     ├─ styles_servicoRecolha.css
│  │     ├─ styles_servicos.css
│  │     └─ styles_servicosVet.css
│  │
│  └─ visits/                   # Secção de marcação de visitas
│     ├─ images/                # Imagens desta secção
│     │  ├─ animais_croae.png
│     │  └─ instalacoes_croae.jpg
│     ├─ visitar.html           # Página para agendar uma visita ao CROAE
│     └─ styles/
│        └─ styles_visitar.css  # Estilos específicos da página de visitas
│
├─ README.md                    # Documento de explicação do projeto
│
└─ styles_gerais/               # CSS global e partilhado por todas as páginas
   ├─ buttons.css               # Estilo dos botões e do botão "Back to Top"
   ├─ font_awesome.css          # Cores e ajustes dos ícones Font Awesome
   ├─ footer.css                # Estilo do rodapé
   ├─ hero.css                  # Estilo da secção "hero" (faixa de destaque)
   ├─ navbar.css                # Estilo da barra de navegação
   ├─ styles_index.css          # CSS específico para o index.html
   └─ tipografia.css            # Definições de fontes e hierarquia tipográfica

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

## 👤 Autor

### Desenvolvido por Leonor Joaquim e André Correia || 2025

[![GitHub](https://img.shields.io/badge/GitHub-leonormcjoaquim-181717?style=for-the-badge&logo=github)](https://github.com/leonormcjoaquim) [![GitHub](https://img.shields.io/badge/GitHub-Brekey53-181717?style=for-the-badge&logo=github)](https://github.com/Brekey53)

```

```
