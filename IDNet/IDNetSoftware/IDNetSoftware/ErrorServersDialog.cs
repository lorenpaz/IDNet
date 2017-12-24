using System;
using System.Collections.Generic;

using DatabaseLibraryS;
using ConstantsLibraryS;

namespace IDNetSoftware
{
    public partial class ErrorServersDialog : Gtk.Dialog
    {
        private Dictionary<string,string> _errores;
		
        //Atributo de las bases de datos propias
		Databases _databases;

        public ErrorServersDialog(Databases databases,Dictionary<string,string> errores)
        {
            this.Build();
            this._errores = errores;
            this._databases = databases;

            if(this._errores.ContainsKey("mysql")){
                textviewErrorMysql.Buffer.Text = this._errores["mysql"];
            }else{
                textviewErrorMysql.Buffer.Text = Constants.NO_ERROR_MYSQL;
            }
                
            if (this._errores.ContainsKey("mongodb"))
            {
                textviewErrorMongoDB.Buffer.Text = this._errores["mongodb"];
            }else{
                textviewErrorMongoDB.Buffer.Text = Constants.NO_ERROR_MONGODB;
            }

        }

        protected void OnButtonCancelClicked(object sender, EventArgs e)
        {
			this.Destroy();
        }
        
        protected void OnButtonOKClicked(object sender, EventArgs e)
        {
            this.Destroy();
        }

		private bool comprobacionServidoresBaseDeDatos()
		{
			bool ok = false;
			string messageError = "";
			foreach (KeyValuePair<string, List<Tuple<string,string,string>>> entry in this._databases.DatabasesPropias)
			{
				switch (entry.Key)
				{
					case "mysql":
						try
						{
							ok = this._databases.ComprobacionMysql();
						}
						catch (Exception e)
						{
                            messageError = e.Message;
                            this._errores["mysql"] = messageError;
						}
                        if(ok)
                        {
                            textviewErrorMysql.Buffer.Text = Constants.NO_ERROR_MYSQL;    
                        }

						break;
					case "mongodb":

						ok = this._databases.ComprobacionMongodb();
                        if(ok)
                        {
                            textviewErrorMysql.Buffer.Text = Constants.NO_ERROR_MONGODB;
                        }else{
                            this._errores["mongodb"] = Constants.UNABLE_CONNECT_MONGODB;
                        }


						break;
				}
			}
            return ok;
		}

    }
}
