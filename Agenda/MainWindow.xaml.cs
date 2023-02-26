using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Agenda
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        private string operacao;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            switch (operacao)
            {
                case "Inserir":
                    {
                        Contato contato = new Contato();
                        contato.Nome = txtNome.Text;
                        contato.Email = txtEmail.Text;
                        contato.Telefone = txtTelefone.Text;

                        using (AgendaEntities context = new AgendaEntities())
                        {
                            context.Contatos.Add(contato);
                            context.SaveChanges();
                        }
                        break;
                    }
                case "Alterar":
                    {
                        using (AgendaEntities context = new AgendaEntities())
                        {
                            Contato contatoAtual = context.Contatos.Find(Convert.ToInt32(txtID.Text));

                            if(contatoAtual != null)
                            {
                                contatoAtual.Nome = txtNome.Text;
                                contatoAtual.Email = txtEmail.Text;
                                contatoAtual.Telefone = txtTelefone.Text;
                                context.SaveChanges();
                            }
                        }
                        break;
                    }
            }
            ListarContatos();
            AtivarBotoes(1);
            LimparCampos();
        }

        private void LimparCampos()
        {
            txtID.IsEnabled = true;
            txtID.Clear();
            txtNome.Clear();
            txtEmail.Clear();
            txtTelefone.Clear();
        }

        private void btnInserir_Click(object sender, RoutedEventArgs e)
        {
            operacao = "Inserir";
            AtivarBotoes(2);
            LimparCampos();
            txtID.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListarContatos();
            AtivarBotoes(1);
        }

        private void ListarContatos()
        {
            using(AgendaEntities context = new AgendaEntities())
            {
                int qtd = context.Contatos.Count();
                lblGridContatos.Content = "Número de Contatos Existentes: "+ qtd.ToString();
                var consulta = context.Contatos;
                gridDados.ItemsSource = consulta.ToList();
            }
        }

        private void AtivarBotoes(int op)
        {
            btnAlterar.IsEnabled = false;
            btnInserir.IsEnabled = false;
            btnExcluir.IsEnabled = false;
            btnCancelar.IsEnabled = false;
            btnLocalizar.IsEnabled = false;
            btnSalvar.IsEnabled = false;

            switch (op)
            {
                case 1:
                    {
                        btnInserir.IsEnabled = true;
                        btnLocalizar.IsEnabled= true;
                        break;
                    }
                case 2: 
                    {
                        btnSalvar.IsEnabled= true;
                        btnCancelar.IsEnabled= true;
                        break;
                    }
                    case 3:
                    {
                        btnAlterar.IsEnabled = true;
                        btnExcluir.IsEnabled = true;
                        break; 
                    }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            AtivarBotoes(1);
            LimparCampos();
        }

        private void btnLocalizar_Click(object sender, RoutedEventArgs e)
        {
            if(txtID.Text.Trim().Count() > 0) 
            {
                try
                {
                    int Id = Convert.ToInt32(txtID.Text);

                    using(AgendaEntities context = new AgendaEntities())
                    {
                        Contato contato = context.Contatos.Find(Id);
                        gridDados.ItemsSource = new Contato[1] { contato };
                    }
                }
                catch (Exception)
                {
                }
            }

            if (txtNome.Text.Trim().Count() > 0)
            {
                try
                {
                    using (AgendaEntities context = new AgendaEntities())
                    {
                        var consulta = from contato in context.Contatos
                                       where contato.Nome.Contains(txtNome.Text) 
                                       select contato;
                        gridDados.ItemsSource = consulta.ToList();
                    }
                }
                catch (Exception)
                {
                }
            }

            if (txtEmail.Text.Trim().Count() > 0)
            {
                try
                {
                    using (AgendaEntities context = new AgendaEntities())
                    {
                        var consulta = from contato in context.Contatos
                                       where contato.Email.Contains(txtEmail.Text)
                                       select contato;
                        gridDados.ItemsSource = consulta.ToList();
                    }
                }
                catch (Exception)
                {
                }
            }

            if (txtTelefone.Text.Trim().Count() > 0)
            {
                try
                {
                    using (AgendaEntities context = new AgendaEntities())
                    {
                        var consulta = from contato in context.Contatos
                                       where contato.Telefone.Contains(txtTelefone.Text)
                                       select contato;
                        gridDados.ItemsSource = consulta.ToList();
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void gridDados_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(gridDados.SelectedIndex >= 0)
            {
                txtID.IsReadOnly = true;

                Contato contato = (Contato)gridDados.SelectedItem;

                txtNome.Text = contato.Nome;
                txtEmail.Text = contato.Email;
                txtTelefone.Text = contato.Telefone;
                txtID.Text = contato.id.ToString();
                AtivarBotoes(3);
            }
        }

        private void btnAlterar_Click(object sender, RoutedEventArgs e)
        {
            operacao = "Alterar";
            AtivarBotoes(2);
            txtID.IsEnabled = false;
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            using(AgendaEntities context = new AgendaEntities())
            {
                Contato contato = context.Contatos.Find(Convert.ToInt32(txtID.Text));

                if(contato != null)
                {
                    context.Contatos.Remove(contato);
                    context.SaveChanges();
                    ListarContatos();
                    AtivarBotoes(1);
                    LimparCampos();
                }
            }
        }
    }
}
