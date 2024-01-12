using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace cadastroDeUsuários
{
    internal class Program
    {
        static string delimitadorInicio;
        static string delimitadorFim;
        static string tagNome;
        static string tagNascimento;
        static string tagNomeDaRua;
        static string tagNumeroDaCasa;
        static string tagNumeroDoDocumento;
        static string caminhoArquivo;
        public struct DadosCadastraisStruct
        {
            public string nome;
            public DateTime dataDeNascimento;
            public string nomeDaRua;
            public UInt32 numeroDaCasa;
            public string numeroDoDocumento;
        }

        public enum Resultado_e
        {
            sucesso = 0,
            sair = 1,
            excecao = 2
        }

        public static Resultado_e PegarString( ref string minhaString, string mensagem)
        {
            Resultado_e retorno;
            Console.WriteLine(mensagem);
            string temp = Console.ReadLine();

            if(temp == "s" || temp =="S")
            {
                retorno = Resultado_e.sair;
            }
            else
            {
                minhaString = temp;
                retorno = Resultado_e.sucesso;
            }
            Console.Clear();
            return retorno;
        }

        public static Resultado_e PegarData(ref DateTime data, string mensagem)
        {
            Resultado_e retorno;

            do
            {

                try
                {
                    Console.WriteLine(mensagem);
                    string temp = Console.ReadLine();

                    if(temp == "s" || temp == "S")
                    {
                        retorno = Resultado_e.sair;
                    }
                    else
                    {
                        data = Convert.ToDateTime(temp);
                        retorno = Resultado_e.sucesso;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Exceção: " + e.Message);
                    Console.WriteLine("Pressione qualquer tecla para continuar");
                    Console.ReadKey(true);
                    Console.Clear();
                    retorno = Resultado_e.excecao;
                }

            } while (retorno == Resultado_e.excecao);
            Console.Clear();
            return retorno;
        }

        public static Resultado_e PegarUInt32(ref UInt32 numero, string mensagem)
        {
            Resultado_e retorno;

            do
            {

                try
                {
                    Console.WriteLine(mensagem);
                    string temp = Console.ReadLine();

                    if (temp == "s" || temp == "S")
                    {
                        retorno = Resultado_e.sair;
                    }
                    else
                    {
                        numero = Convert.ToUInt32(temp);
                        retorno = Resultado_e.sucesso;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exceção: " + e.Message);
                    Console.WriteLine("Pressione qualquer tecla para continuar");
                    Console.ReadKey(true);
                    Console.Clear();
                    retorno = Resultado_e.excecao;
                }

            } while (retorno == Resultado_e.excecao);
            Console.Clear();
            return retorno;
        }

        public static void MostrarMensagem(string mensagem)
        {
            Console.WriteLine(mensagem);
            Console.WriteLine("Pressione qualquer tecla para continuar");
            Console.ReadKey(true);
            Console.Clear();
        }

      public static Resultado_e Cadastrausuario(ref List<DadosCadastraisStruct> ListaDeusuarios)
        {
            DadosCadastraisStruct CadastroUsuario;
            CadastroUsuario.nome = "";
            CadastroUsuario.dataDeNascimento = new DateTime();
            CadastroUsuario.nomeDaRua = "";
            CadastroUsuario.numeroDaCasa = 0;
            CadastroUsuario.numeroDoDocumento = "";

            if(PegarString(ref CadastroUsuario.nome, "Digite o nome completo ou S para sair") == Resultado_e.sair)
            {
                return Resultado_e.sair;
            }
            if(PegarData(ref CadastroUsuario.dataDeNascimento, "Digite a data de nascimento no formato DD/MM/AAAA ou pressione S para sair") == Resultado_e.sair)
            {
                return Resultado_e.sair;
            }
            if(PegarString(ref CadastroUsuario.nomeDaRua, "Digite o nome da rua ou S para sair") == Resultado_e.sair)
            {
                return Resultado_e.sair;
            }
            if(PegarUInt32(ref CadastroUsuario.numeroDaCasa, "Digite o número da casa ou S para sair") == Resultado_e.sair)
            {
                return Resultado_e.sair;
            }
            if(PegarString(ref CadastroUsuario.numeroDoDocumento, "Digite o número do cpf ou S para sair") == Resultado_e.sair)
            {
                return Resultado_e.sair;
            }
            ListaDeusuarios.Add(CadastroUsuario);

            GravaDados(caminhoArquivo, ref ListaDeusuarios);
            return Resultado_e.sucesso;
        }

        public static void GravaDados(string caminho, ref List<DadosCadastraisStruct> ListaDeUsuarios)
        {
            try
            {
                string conteudoArquivo = "";
                foreach(DadosCadastraisStruct cadastro in ListaDeUsuarios)
                {
                    conteudoArquivo += delimitadorInicio + "\r\n";
                    conteudoArquivo += tagNome + cadastro.nome + "\r\n";
                    conteudoArquivo += tagNascimento + cadastro.dataDeNascimento.ToString("dd/MM/yyyy") + "\r\n";
                    conteudoArquivo += tagNomeDaRua + cadastro.nomeDaRua + "\r\n";
                    conteudoArquivo += tagNumeroDaCasa + cadastro.numeroDaCasa.ToString() + "\r\n";
                    conteudoArquivo += tagNumeroDoDocumento + cadastro.numeroDoDocumento + "\r\n";
                    conteudoArquivo += delimitadorFim + "\r\n";
                }
                File.WriteAllText(caminho, conteudoArquivo);
            }
            catch(Exception e)
            {
                Console.WriteLine("EXCEÇÂO: " + e.Message);
            }
        }

        public static void carregaDados(string caminho, ref List<DadosCadastraisStruct> ListaDeUsuarios)
        {
            try
            {
                if(File.Exists(caminho))
                {
                    string[] conteudoArquivo = File.ReadAllLines(caminho);
                    DadosCadastraisStruct dadosCadastrais;
                    dadosCadastrais.nome = "";
                    dadosCadastrais.dataDeNascimento = new DateTime();
                    dadosCadastrais.nomeDaRua = "";
                    dadosCadastrais.numeroDaCasa = 0;
                    dadosCadastrais.numeroDoDocumento = "";
                    foreach(string linha in conteudoArquivo)
                    {
                        if (linha.Contains(delimitadorInicio))
                        {
                            continue;
                        }
                        if (linha.Contains(delimitadorFim))
                        {
                            ListaDeUsuarios.Add(dadosCadastrais);
                        }
                        if (linha.Contains(tagNome))
                        {
                            dadosCadastrais.nome = linha.Replace(tagNome, "");
                        }
                        if (linha.Contains(tagNascimento))
                        {
                            dadosCadastrais.dataDeNascimento = Convert.ToDateTime(linha.Replace(tagNascimento, ""));
                        }
                        if (linha.Contains(tagNomeDaRua))
                        {
                            dadosCadastrais.nomeDaRua = linha.Replace(tagNomeDaRua, "");
                        }
                        if (linha.Contains(tagNumeroDaCasa))
                        {
                            dadosCadastrais.numeroDaCasa = Convert.ToUInt32(linha.Replace(tagNumeroDaCasa, ""));
                        }
                        if (linha.Contains(tagNumeroDoDocumento))
                        {
                            dadosCadastrais.numeroDoDocumento = linha.Replace(tagNumeroDoDocumento, "");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("EXCEÇÂO: " + e.Message);
            }
        }

        public static void BuscaUsuarioPeloDoc(List<DadosCadastraisStruct> ListaDeUsuarios)
        {
            Console.WriteLine("Digite o número do documento para buscar o usuário ou S para sair");
            string temp = Console.ReadLine();
            if(temp.ToLower() == "s")
            {
                return;
            }
            else
            {
                List<DadosCadastraisStruct> ListaDeUsuariosTemp = ListaDeUsuarios.Where(x => x.numeroDoDocumento == temp).ToList();
                if(ListaDeUsuariosTemp.Count > 0)
                {
                    foreach(DadosCadastraisStruct usuario in ListaDeUsuariosTemp)
                    {
                        Console.WriteLine(tagNome + usuario.nome);
                        Console.WriteLine(tagNascimento + usuario.dataDeNascimento.ToString("dd/MM/YYYY"));
                        Console.WriteLine(tagNomeDaRua + usuario.nomeDaRua);
                        Console.WriteLine(tagNumeroDaCasa + usuario.numeroDaCasa.ToString());

                    }
                }
                else
                {
                    Console.WriteLine("Nenhum usuário possui o documento: "+ temp);
                }
                MostrarMensagem("");
            }
        }

        public static void ExcluiUsuarioPeloDoc(ref List<DadosCadastraisStruct> ListaDeUsuarios)
        {
            Console.WriteLine("Digite o número do documento para excluir o usuário ou S para sair");
            string temp = Console.ReadLine();
            bool usuarioExcluido = false;
            
            if(temp.ToLower() == "s")
            {
                return;
            }
            else
            {
                List<DadosCadastraisStruct> ListaDeUsuariosTemp = ListaDeUsuarios.Where(x => x.numeroDoDocumento == temp).ToList();
                if(ListaDeUsuariosTemp.Count > 0)
                {
                    foreach(DadosCadastraisStruct usuario in ListaDeUsuariosTemp)
                    {
                        ListaDeUsuarios.Remove(usuario);
                        usuarioExcluido = true;
                        Console.WriteLine("Usuário excluído com sucesso");
                    }
                    if (usuarioExcluido)
                    {
                        GravaDados(caminhoArquivo, ref ListaDeUsuarios);
                    }
                }
                else
                {
                    Console.WriteLine("Nenhum usuário possui o documento: "+ temp);
                }
                MostrarMensagem("");
            }
        }

        static void Main(string[] args)
        {
            List<DadosCadastraisStruct> ListaDeUsuarios = new List<DadosCadastraisStruct>();
            string opcao = "";
            delimitadorInicio = "##### INÍCIO #####";
            delimitadorFim = " ##### FIM #####";
            tagNome = "NOME: ";
            tagNascimento = "DATA_DE_NASCIMENTO: ";
            tagNomeDaRua = "NOME_DA_RUA: ";
            tagNumeroDaCasa = "NUMERO_DA_CASA: ";
            tagNumeroDoDocumento = "NUMERO_DO_DOCUMENTO: ";
            caminhoArquivo = @"baseDeDados.txt";

            carregaDados(caminhoArquivo, ref ListaDeUsuarios);


            do
            {
                Console.WriteLine("pressione C para cadastrar um novo usuário");
                Console.WriteLine("Pressione B para buscar um usuário");
                Console.WriteLine("Pressione E para excluir um usuário");
                Console.WriteLine("Pressione S para sair");

                opcao = Console.ReadKey(true).KeyChar.ToString().ToLower();

                if(opcao == "c")
                {
                    Cadastrausuario(ref ListaDeUsuarios);
                }
                else if (opcao == "b")
                {
                    //buscar usuario
                    BuscaUsuarioPeloDoc(ListaDeUsuarios);
                }
                else if (opcao == "e")
                {
                    //excluir usuario
                    ExcluiUsuarioPeloDoc(ref ListaDeUsuarios);
                }
                else if (opcao == "s")
                {
                    MostrarMensagem("Encerrando o programa");
                }
                else
                {
                    MostrarMensagem("Opção desconhecida!");
                }

            } while (opcao != "s");
        }
    }
}
