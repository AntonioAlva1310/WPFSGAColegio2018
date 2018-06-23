using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;
using SGA2018.Model;
using System.Collections.ObjectModel;
using System.Data.Entity;



namespace SGA2018.ViewModel
{

    


    public class PuestoViewModel : INotifyPropertyChanged, ICommand
    {

        #region "Datos"
        private SGADataContext _db = new SGADataContext();
        #endregion
        #region "Campos"
        private Puesto _elemento;
        private IDialogCoordinator _dialogCoordinator;
        private string _titulo;
        private string _descripcion;
        private PuestoViewModel _instancia;
        private ObservableCollection<Puesto> _listaPuestos;
        private ACCION _accion = ACCION.NINGUNO;
        #endregion

        #region "Propiedades"
        public Puesto Elemento
        {
            get
            {
                return _elemento;
            }
            set
            {
                _elemento = value;
                if (value != null)
                {
                    this.Descripcion = value.Descripcion;
                }
                NotificarCambio("Elemento");

            }
        }
        public PuestoViewModel Instancia

        {
            get
            {
                return _instancia;
            }
            set
            {

                _instancia = value;
                NotificarCambio("Instancia");
            }
        }

        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; NotificarCambio("Titulo"); }
        }

        public string Descripcion
        {
            get
            {
                return _descripcion;
            }
            set
            {
                _descripcion = value;
                NotificarCambio("Descripcion");
            }
        }
        public ObservableCollection<Puesto> ListaPuestos
        {
            get
            {
                if (_listaPuestos != null)
                {
                    return _listaPuestos;
                }
                else
                {
                    _listaPuestos = new ObservableCollection<Puesto>(_db.Puestos.ToList());
                    return _listaPuestos;
                }
            }
            set
            {
                _listaPuestos = value;
                NotificarCambio("ListaPuestos");
            }
        }
        #endregion 
        #region "Constructores"
        public PuestoViewModel(IDialogCoordinator dialogCoordinator)
        {
            this._dialogCoordinator = dialogCoordinator;
            this.Titulo = "Sistema SGA";

            this.Instancia = this;
        }
        #endregion
        #region "Metodos y funciones"
        

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;//los click que ejectuare


             public void NotificarCambio(string propiedad)
           {
         if (PropertyChanged != null)
       {
        PropertyChanged(this, new PropertyChangedEventArgs(propiedad));
                }
      }
        //ejecutar instruccion con algún boton relacionado 
        public bool CanExecute(object parameter)
        {
           return true;//le decimos que si lo ejecute 
        }

        public async void Execute(object control)
        {
            if (control.Equals("Nuevo"))
            {
                //ActivarControles();
                this._accion = ACCION.NUEVO;
            }
            else if (control.Equals("Eliminar"))
            {
                if (Elemento != null)
                {
                    MessageDialogResult resultado = await this._dialogCoordinator.ShowMessageAsync(this
                    , "Eliminar Puesto"
                    , "Está seguro de eliminar el registro?"
                    , MessageDialogStyle.AffirmativeAndNegative);
                    if (resultado == MessageDialogResult.Affirmative)
                    {
                        try
                        {
                            _db.Puestos.Remove(Elemento);
                            _db.SaveChanges();
                            ListaPuestos.Remove(Elemento);
                            //LimpiarCampos();

                        }
                        catch (Exception ex)
                        {
                            await this._dialogCoordinator.ShowMessageAsync(
                            this
                            , "Eliminar Puesto"
                            , ex.Message);
                        }

                    }
                }
                else
                {
                    await this._dialogCoordinator.ShowMessageAsync(
                     this
                    , "Eliminar Puesto"
                    , "Debe seleccionar un elemento");
                }
            }
            else if (control.Equals("Guardar"))
            {
                switch (this._accion)
                {
                    case ACCION.NUEVO:
                        try
                        {
                            var registro = new Puesto
                            {
                                Descripcion = this.Descripcion

                            };
                            _db.Puestos.Add(registro);
                            _db.SaveChanges();
                            this.ListaPuestos.Add(registro);
                        }
                        catch (Exception ex)
                        {
                            await this._dialogCoordinator.ShowMessageAsync(
                            this
                            , "Guardar Puesto"
                            , ex.Message);
                        }
                        finally
                        {
                            // DesactivarControles();
                            this._accion = ACCION.NINGUNO;
                        }
                        break;
                    case ACCION.GUARDAR:
                        try
                        {
                            int posicion = ListaPuestos.IndexOf(Elemento);
                            var registro = _db.Puestos.Find(Elemento.PuestoId);
                            if (registro != null)
                            {
                                registro.Descripcion = this.Descripcion;


                                _db.Entry(registro).State = EntityState.Modified;
                                _db.SaveChanges();
                                ListaPuestos.RemoveAt(posicion);
                                ListaPuestos.Insert(posicion, registro);
                            }

                        }
                        catch (Exception ex)
                        {
                            await this._dialogCoordinator.ShowMessageAsync(
                            this
                            , "Editar Puesto"
                            , ex.Message);
                        }
                        finally
                        {
                            //DesactivarControles();
                            this._accion = ACCION.NINGUNO;
                        }
                        break;
                }

            }

            else if (control.Equals("Editar"))
            {
                if (Elemento != null)
                {
                    //  ActivarControles();
                    //   this.IsReadOnlyCarne = true;
                    this._accion = ACCION.GUARDAR;
                }
                else
                {
                    await this._dialogCoordinator.ShowMessageAsync(
                    this
                    , "Editar Puesto"
                    , "Debe seleccionar un elemento");
                }
            }
        }


        #endregion
    }

}





