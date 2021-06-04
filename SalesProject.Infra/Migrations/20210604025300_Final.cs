using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesProject.Infra.Migrations
{
    public partial class Final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id gerado pela aplicação."),
                    Cnpj = table.Column<string>(type: "varchar(18)", maxLength: 18, nullable: false, comment: "Número do cadastro nacional de pessoa jurídica do cliente."),
                    Empresa = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "Nome da empresa do cliente."),
                    InscricaoEstadual = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, comment: "Inscrição estadual da empresa do cliente."),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "E-mail da empresa do cliente"),
                    DataAbertura = table.Column<DateTime>(type: "date", nullable: true, comment: "Data de abertura da empresa do cliente."),
                    Telefone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "Telefone da empresa do cliente."),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false, comment: "Data de cadastrado do cliente no sistema."),
                    InscricaoMunicipal = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true, comment: "Inscrição municipal da empresa do cliente.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id gerado pela aplicação."),
                    Nome = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, comment: "Primeiro nome do contato."),
                    Sobrenome = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "Último nome do contato."),
                    NomeSobrenome = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true, comment: "Primeiro nome e último nome"),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "E-mail do contato."),
                    WhatsApp = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "Número do WhatsApp do contato."),
                    Telefone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, comment: "Telefone do contato."),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Vínculo com a tabela de Cliente (Fk).")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contato_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id gerado pela aplicação."),
                    Descricao = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "Descrição do endereço."),
                    Cep = table.Column<string>(type: "varchar(18)", maxLength: 18, nullable: false, comment: "Código postal do endereço."),
                    Tipo = table.Column<int>(type: "int", nullable: true, comment: "Tipo do endereço. 1: Cobrança, 2: Entrega, 3: Outro."),
                    Logradouro = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false, comment: "Logradouro do endereço."),
                    Bairro = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, comment: "Bairro do endereço."),
                    Numero = table.Column<int>(type: "int", nullable: false, comment: "Número do endereço."),
                    Cidade = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, comment: "Cidade do endereço."),
                    CodigoIbgeCidade = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, comment: "Código IBGE da cidade. Usado para emissão de NF."),
                    Uf = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false, comment: "Estado do endereço."),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Vínculo com a tabela de Cliente (Fk).")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endereco_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id gerado pela aplicação."),
                    DataLancamento = table.Column<DateTime>(type: "date", nullable: false, comment: "Data de Lançamento do Pedido."),
                    DataEntrega = table.Column<DateTime>(type: "date", nullable: false, comment: "Data de Entrega do Pedido."),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "Status do Pedido. 1:Aberto, 2:Aprovado, 3:Faturado, 4:Cancelado."),
                    Total = table.Column<decimal>(type: "money", nullable: false, comment: "Total do Pedido."),
                    Observacao = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true, comment: "Observações para o Pedido."),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Vínculo com a tabela de Cliente (Fk).")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedido_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id gerado pela aplicação."),
                    Nome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "Nome do Produto."),
                    CodigoNcm = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true, comment: "Código NCM do Produto."),
                    PrecoCombinado = table.Column<decimal>(type: "money", nullable: false, comment: "Preço combinado do Produto."),
                    CustosAdicionais = table.Column<decimal>(type: "money", nullable: false, comment: "Custos adicionais combinado para o Produto."),
                    PrevisaoMensal = table.Column<double>(type: "float", nullable: false, comment: "Previsão mensal mínima combinada para o Produto."),
                    Detalhes = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true, comment: "Detalhes do Produto."),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Vínculo com a tabela de Cliente (Fk).")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produto_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id gerado pela aplicação."),
                    Usuario = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "Nome de usuário para realizar login no sistema."),
                    Nome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "Nome do usuário."),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "E-mail do usuário"),
                    SenhaCriptografada = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false, comment: "Senha criptografada do usuário"),
                    Funcao = table.Column<int>(type: "int", nullable: false, comment: "Função do usuário. Atribui as permissões do usuário no sistema. 1:Cliente, 2:Vendedor, 3:Funcinário de TI, 4:Gestor."),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Vínculo com a tabela de Cliente (Fk).")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotaFiscal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id gerado pela aplicação."),
                    PedidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Vínculo com a tabela de Pedido (Fk)."),
                    DataEmissao = table.Column<DateTime>(type: "date", nullable: false, comment: "Data de Emissão da Nota fiscal."),
                    NaturezaOperacao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, comment: "Natureza de Operação da Nota fiscal."),
                    BaseCalculoIcms = table.Column<decimal>(type: "money", nullable: false, comment: "Base para Calculo do Icms."),
                    ValorIcms = table.Column<decimal>(type: "money", nullable: false, comment: "Valor total do Icms na Nota fiscal."),
                    TotalProdutos = table.Column<decimal>(type: "money", nullable: false, comment: "Valor total dos produtos da Nota fiscal."),
                    TotalNota = table.Column<decimal>(type: "money", nullable: false, comment: "Valor total da Nota fiscal."),
                    NotaEnviada = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false, comment: "Nota fiscal foi enviada para Sefaz? Y: Sim, N: Não."),
                    NotaIdPlugNotas = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true, comment: "Chave da Nota Fiscal integrada com Plug Notas Api.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotaFiscal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotaFiscal_Pedido_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ItemPedido",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id gerado pela aplicação."),
                    PedidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Vínculo com a tabela de Pedido (Fk)."),
                    Quantidade = table.Column<int>(type: "int", nullable: false, comment: "Quantidade do item do Pedido."),
                    ValorUnitario = table.Column<decimal>(type: "money", nullable: false, comment: "Valor unitário do item do Pedido."),
                    CustosAdicionais = table.Column<decimal>(type: "money", nullable: false, comment: "Custos adicionais do item do Pedido."),
                    TotalItem = table.Column<decimal>(type: "money", nullable: false, comment: "Total do item do Pedido."),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "Vínculo com a tabela de Produto (Fk).")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPedido_Pedido_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPedido_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ItemNotaFiscal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Id gerado pela aplicação."),
                    NotaFiscalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Vínculo com a tabela de Nota fiscal (Fk)."),
                    NomeProduto = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "Nome do produto do item da Nota fiscal."),
                    CodigoNcm = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true, comment: "Código Ncm do item da Nota fiscal."),
                    Quantidade = table.Column<int>(type: "int", nullable: false, comment: "Quantidade do item da Nota fiscal."),
                    ValorUnitario = table.Column<decimal>(type: "money", nullable: false, comment: "Valor Unitário do item da Nota fiscal."),
                    ValorTotal = table.Column<decimal>(type: "money", nullable: false, comment: "Valor total do item da Nota fiscal."),
                    ImpostoTotal = table.Column<decimal>(type: "money", nullable: false, comment: "Imposto total do item da Nota fiscal."),
                    CustosAdicionais = table.Column<decimal>(type: "money", nullable: false, comment: "Custos adicionais do item da Nota fiscal."),
                    OrigemIcms = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false, comment: "Origem Icms do item da Nota fiscal."),
                    CstIcms = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false, comment: "Cst Icms do item da Nota fiscal."),
                    ModalidadeDeterminacao = table.Column<int>(type: "int", nullable: false, comment: "Modalidade Determinacao do item da Nota fiscal."),
                    BaseCalcIcms = table.Column<decimal>(type: "money", nullable: false, comment: "Base Cálculo do Icms do item da Nota fiscal."),
                    AliquotaIcms = table.Column<double>(type: "float", nullable: false, comment: "Alíquota do Icms do item da Nota fiscal."),
                    ValorIcms = table.Column<decimal>(type: "money", nullable: false, comment: "Valor do Icms do item da Nota fiscal."),
                    CstPis = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true, comment: "Cst Pis do item da Nota fiscal."),
                    BaseCalcPis = table.Column<decimal>(type: "money", nullable: false, comment: "Base Cálculo do Pis do item da Nota fiscal."),
                    AliquotaPis = table.Column<double>(type: "float", nullable: false, comment: "Alíquota do Pis do item da Nota fiscal."),
                    ValorPis = table.Column<decimal>(type: "money", nullable: false, comment: "Valor do Pis do item da Nota fiscal."),
                    CstCofins = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true, comment: "Cst Cofins do item da Nota fiscal."),
                    ValorBaseCalcCofins = table.Column<decimal>(type: "money", nullable: false, comment: "Valor Base Cálculo do Cofins do item da Nota fiscal."),
                    AliquotaCofins = table.Column<double>(type: "float", nullable: false, comment: "Alíquota do Cofins do item da Nota fiscal."),
                    ValorCofins = table.Column<decimal>(type: "money", nullable: false, comment: "Valor do Cofins do item da Nota fiscal.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemNotaFiscal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemNotaFiscal_NotaFiscal_NotaFiscalId",
                        column: x => x.NotaFiscalId,
                        principalTable: "NotaFiscal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contato_ClienteId",
                table: "Contato",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_ClienteId",
                table: "Endereco",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemNotaFiscal_NotaFiscalId",
                table: "ItemNotaFiscal",
                column: "NotaFiscalId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedido_PedidoId",
                table: "ItemPedido",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedido_ProdutoId",
                table: "ItemPedido",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_NotaFiscal_PedidoId",
                table: "NotaFiscal",
                column: "PedidoId",
                unique: true,
                filter: "[PedidoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_ClienteId",
                table: "Pedido",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_Status_DataLancamento",
                table: "Pedido",
                columns: new[] { "Status", "DataLancamento" });

            migrationBuilder.CreateIndex(
                name: "IX_Produto_ClienteId",
                table: "Produto",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_ClienteId",
                table: "Usuario",
                column: "ClienteId",
                unique: true,
                filter: "[ClienteId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contato");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "ItemNotaFiscal");

            migrationBuilder.DropTable(
                name: "ItemPedido");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "NotaFiscal");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
