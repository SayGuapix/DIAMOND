using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.IO;
using System.Linq;
using System.Data;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace diamondAPP
{
	class Program
	{
		class Usuario
		{
			public string Nombre { get; set; }
			public string Contraseña { get; set; }
			public string Correo { get; set; }
			public string Celular { get; set; }
			public string Puntos { get; set; }

			public Usuario(string nombre, string contraseña, string correo, string celular, string puntos)
			{
				Nombre = nombre;
				Contraseña = contraseña;
				Correo = correo;
				Celular = celular;
				Puntos = puntos;
			}
		}
		
		//globales
		static int opcion = 1;
		static int anterior;
		static int fila, columna;
		
		static ConsoleKeyInfo tecla;
		
		static string usuarioBuscar = "";
		static string usuario = "";
		static string contraseña = "";
		static string correo = "", correoPascual = "";
		static string celular = "";
		static string puntos = "";
		static int puntosObtenidos = 0;
		
		static bool usuarioValido = false;
		static bool correoValido = false;
		static bool celularValido = false;
		static bool contraseñaValida = false;
		static bool cuentaCreada = false;
		
		static bool salir = false;
		static bool mostrar = true;
		static string idioma = "español";
		
		static string pregunta = "";
		static int chats = 0;
		static bool entrarChat = false;
		static double promedio;
		static string preguntaAnterior = "";
		static bool nuevo = true;
		static bool notificacion1 = true;
		
		static bool mostrar_movimientos = false;
		static bool mostrar_rondas = true;
		static bool mostrar_pos = false;
		static bool mostrar_victorias = true;
		static bool pantalla_empates = false;
		static bool pantalla_victorias = false;
		
		static void Precionar()
		{
			tecla = Console.ReadKey(true);
		}
		
		public static void Main(string[] args)
		{
			Console.Title = "DIAMOND";
			
			string archivo = "Datos.txt";
			// Verificar si el archivo existe, si no, crearlo
			if (!File.Exists(archivo)) {
				using (StreamWriter sw = File.CreateText(archivo)) {
					Console.WriteLine("Archivo " + archivo + " creado exitosamente.");
				}
			}
			
			Cursor(false);
			
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			Console.SetWindowSize(Console.WindowWidth, Console.WindowHeight);
			Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
			Console.WindowWidth = 120; // Ancho de la ventana en caracteres
			Console.WindowHeight = 30; // Alto de la ventana en líneas
			
			
			string[] arteASCII =
			{
				"██████████████████████████████████████████████████████████████████████████████████████████████████████████████████████",
				"█░░░░░░░░░░░░███░░░░░░░░░░█░░░░░░░░░░░░░░█░░░░░░██████████░░░░░░█░░░░░░░░░░░░░░█░░░░░░██████████░░░░░░█░░░░░░░░░░░░███",
				"█░░▄▀▄▀▄▀▄▀░░░░█░░▄▀▄▀▄▀░░█░░▄▀▄▀▄▀▄▀▄▀░░█░░▄▀░░░░░░░░░░░░░░▄▀░░█░░▄▀▄▀▄▀▄▀▄▀░░█░░▄▀░░░░░░░░░░██░░▄▀░░█░░▄▀▄▀▄▀▄▀░░░░█",
				"█░░▄▀░░░░▄▀▄▀░░█░░░░▄▀░░░░█░░▄▀░░░░░░▄▀░░█░░▄▀▄▀▄▀▄▀▄▀▄▀▄▀▄▀▄▀░░█░░▄▀░░░░░░▄▀░░█░░▄▀▄▀▄▀▄▀▄▀░░██░░▄▀░░█░░▄▀░░░░▄▀▄▀░░█",
				"█░░▄▀░░██░░▄▀░░███░░▄▀░░███░░▄▀░░██░░▄▀░░█░░▄▀░░░░░░▄▀░░░░░░▄▀░░█░░▄▀░░██░░▄▀░░█░░▄▀░░░░░░▄▀░░██░░▄▀░░█░░▄▀░░██░░▄▀░░█",
				"█░░▄▀░░██░░▄▀░░███░░▄▀░░███░░▄▀░░░░░░▄▀░░█░░▄▀░░██░░▄▀░░██░░▄▀░░█░░▄▀░░██░░▄▀░░█░░▄▀░░██░░▄▀░░██░░▄▀░░█░░▄▀░░██░░▄▀░░█",
				"█░░▄▀░░██░░▄▀░░███░░▄▀░░███░░▄▀▄▀▄▀▄▀▄▀░░█░░▄▀░░██░░▄▀░░██░░▄▀░░█░░▄▀░░██░░▄▀░░█░░▄▀░░██░░▄▀░░██░░▄▀░░█░░▄▀░░██░░▄▀░░█",
				"█░░▄▀░░██░░▄▀░░███░░▄▀░░███░░▄▀░░░░░░▄▀░░█░░▄▀░░██░░░░░░██░░▄▀░░█░░▄▀░░██░░▄▀░░█░░▄▀░░██░░▄▀░░██░░▄▀░░█░░▄▀░░██░░▄▀░░█",
				"█░░▄▀░░██░░▄▀░░███░░▄▀░░███░░▄▀░░██░░▄▀░░█░░▄▀░░██████████░░▄▀░░█░░▄▀░░██░░▄▀░░█░░▄▀░░██░░▄▀░░░░░░▄▀░░█░░▄▀░░██░░▄▀░░█",
				"█░░▄▀░░░░▄▀▄▀░░█░░░░▄▀░░░░█░░▄▀░░██░░▄▀░░█░░▄▀░░██████████░░▄▀░░█░░▄▀░░░░░░▄▀░░█░░▄▀░░██░░▄▀▄▀▄▀▄▀▄▀░░█░░▄▀░░░░▄▀▄▀░░█",
				"█░░▄▀▄▀▄▀▄▀░░░░█░░▄▀▄▀▄▀░░█░░▄▀░░██░░▄▀░░█░░▄▀░░██████████░░▄▀░░█░░▄▀▄▀▄▀▄▀▄▀░░█░░▄▀░░██░░░░░░░░░░▄▀░░█░░▄▀▄▀▄▀▄▀░░░░█",
				"█░░░░░░░░░░░░███░░░░░░░░░░█░░░░░░██░░░░░░█░░░░░░██████████░░░░░░█░░░░░░░░░░░░░░█░░░░░░██████████░░░░░░█░░░░░░░░░░░░███",
				"██████████████████████████████████████████████████████████████████████████████████████████████████████████████████████"
			};
			
			int contador = 0;
			mostrar = false;

			fila = 20; //carga con colores
			for( columna = 1; columna<=118; columna=columna+1 ){
				Color("azul", "grisoscuro");
				Posicion(columna, fila);Console.WriteLine(" ");
			}
			
			fila = 20; //carga con colores
			for( columna = 1; columna<=115; columna=columna+2 ){
				Color("azul", "blanco");
				Posicion(columna, fila);Console.WriteLine("♦ ");
				Tiempo(10);
				
				for (int i = 0; i < arteASCII.Length; i++)
				{
					ColorCarga();
					Posicion(1, 7 + i);
					Console.WriteLine(arteASCII[i]);
				}
				Color("cian", "grisoscuro");
				Posicion(116,20);
				Console.WriteLine(contador);
				
				if(contador < 99)
				{
					contador++;
					contador++;
				}
			}
			
			Color("negro", "negro");
			Reset();
			
			Color("negro", "blanco");
			for (fila = 3; fila <=19; fila++) {
				for (columna = 36; columna <= 84; columna++) {
					Posicion(columna, fila);
					Console.WriteLine(" ");
				}
			}
			
			do
			{
				
				// Cargar y mostrar datos de todos los usuarios
				string[] datosUsuarios = Cargar("Datos.txt");
				
				
				Cursor(false);
				Sesion();
				
				Precionar();
				
				if(tecla.KeyChar == 'r' || tecla.KeyChar == 'R')
				{
					Reiniciar();
				}
				
				if(tecla.Key == ConsoleKey.Enter)
				{
					switch(opcion)
					{
						case 3: //iniciar sesion
							
							if(idioma == "español")
							{
								if(usuario == "" && contraseña == "")
								{
									Posicion(45,21);
									Color("blanco", "verdeoscuro");
									Console.WriteLine("DIGITA TUS DATOS PARA CONTINUAR   ");
									
									Posicion(51,11);
									Color("rojooscuro", "blanco");
									Console.WriteLine("!");
									
									Posicion(54,14);
									Console.WriteLine("!");
									
									Precionar();
								}
								else if(contraseña == "" &&  usuario != "")
								{
									Posicion(43,21);
									Color("blanco", "verdeoscuro");
									Console.WriteLine("DIGITA TU CONTRASEÑA PARA CONTINUAR    ");
									
									Posicion(54,14);
									Color("rojooscuro", "blanco");
									Console.WriteLine("!");
									
									Precionar();
								}
								else if(contraseña != "" &&  usuario == "")
								{
									Posicion(44,21);
									Color("blanco", "verdeoscuro");
									Console.WriteLine("DIGITA TU USUARIO PARA CONTINUAR    ");
									
									Posicion(51,11);
									Color("rojooscuro", "blanco");
									Console.WriteLine("!");
									
									Precionar();
								}
								else if (IniciarSesion(usuario, contraseña))
								{
									Inicio();
									
									usuario = "";
									contraseña = "";
								}
								else if (!Existe(usuario) || usuario == contraseña)
								{
									Posicion(49,21);
									Color("blanco", "verdeoscuro");
									Console.WriteLine("USUARIO NO ENCONTRADO");
									
									Color("negro", "negro");
									Precionar();
								}
							}
							else if(idioma == "english")
							{
								if(usuario == "" && contraseña == "")
								{
									Posicion(43,21);
									Color("blanco", "verdeoscuro");
									Console.WriteLine("ENTER YOUR INFORMATION TO CONTINUE  ");
									
									Posicion(48,11);
									Color("rojooscuro", "blanco");
									Console.WriteLine("!");
									
									Posicion(48,14);
									Console.WriteLine("!");
									
									Precionar();
								}
								else if(contraseña == "" &&  usuario != "")
								{
									Posicion(44,21);
									Color("blanco", "verdeoscuro");
									Console.WriteLine(" ENTER YOUR PASSWORD TO CONTINUE     ");
									
									Posicion(48,14);
									Color("rojooscuro", "blanco");
									Console.WriteLine("!");
									
									Precionar();
								}
								else if(contraseña != "" &&  usuario == "")
								{
									Posicion(43,21);
									Color("blanco", "verdeoscuro");
									Console.WriteLine("   ENTER YOUR USER ID TO CONTINUE      ");
									
									Posicion(48,11);
									Color("rojooscuro", "blanco");
									Console.WriteLine("!");
									
									Precionar();
								}
								else if (!Existe(usuario))
								{
									Posicion(52,21);
									Color("blanco", "verdeoscuro");
									Console.WriteLine(" USER NOT FOUND      ");
									
									Color("negro", "negro");
									Precionar();
								}
								
								else if (IniciarSesion(usuario, contraseña))
								{
									Inicio();
									
									usuario = "";
									contraseña = "";
								}
							}

							
							Color("negro", "negro");
							
							break;
							
						case 4: //registrarse
							
							mostrar = true;
							Registrarse();
							break;
							
						case 5: //ayuda
							
							AyudaInicio();
							break;
							
						case 6:

							if(tecla.Key == ConsoleKey.Enter)
							{
								
								Posicion(6,28);
								Console.WriteLine("        ");
								
								switch(tecla.Key)
								{
									case ConsoleKey.Enter:
										
										if(idioma == "español")
										{
											idioma = "english";
											
											Color("negro", "negro");
											Reset();
											
											Color("negro", "blanco");
											for (fila = 3; fila <=19; fila++) {
												for (columna = 36; columna <= 84; columna++) {
													Posicion(columna, fila);
													Console.WriteLine(" ");
												}
											}
											
											Posicion(48,11);
											Color("negro", "blanco");
											Console.WriteLine(usuario);
											
											if(mostrar == false && contraseña != "")
											{
												Color("gris", "blanco");
												Posicion(48,14);
												Console.WriteLine("hidden");
											}
											else
											{
												Posicion(48,14);
												Console.WriteLine(contraseña);
											}
											
											break;
										}
										if(idioma == "english")
										{
											idioma = "español";
											
											Color("negro", "negro");
											Reset();
											
											Color("negro", "blanco");
											for (fila = 3; fila <=19; fila++) {
												for (columna = 36; columna <= 84; columna++) {
													Posicion(columna, fila);
													Console.WriteLine(" ");
												}
											}
											
											Posicion(51,11);
											Color("negro", "blanco");
											Console.WriteLine(usuario);
											
											if(mostrar == false && contraseña != "")
											{
												Color("gris", "blanco");
												Posicion(54,14);
												Console.WriteLine("oculta");
											}
											else
											{
												Posicion(54,14);
												Console.WriteLine(contraseña);
											}
											
											break;
										}
										break;
								}
							}
							
							break;
					}
				}
				
				switch(tecla.Key)
				{
					case ConsoleKey.Escape:
						
//						salir = true;
						break;
						
					case ConsoleKey.UpArrow:
						
						opcion = Math.Max(opcion - 1, 1);
						break;
						
					case ConsoleKey.DownArrow:
						
						opcion = Math.Min(opcion + 1, 4);
						break;
						
					case ConsoleKey.RightArrow:
						
						if(opcion == 6)
						{
							opcion = 4;
						}
						else
						{
							opcion = 5;
						}
						break;
						
					case ConsoleKey.LeftArrow:
						
						if(opcion == 5)
						{
							opcion = 4;
						}
						else
						{
							opcion = 6;
						}
						break;
				}

			}while(salir == false);

		}//fin main
		
		static void Inicio()
		{
			// Cargar y mostrar datos de todos los usuarios
			string[] datosUsuarios = Cargar("Datos.txt");
			
			Color("negro", "gris");
			Reset();
			tuiInicio();
			
			opcion = 1;
			
			do
			{
				Cursor(false);
				
				Color("negro", "blanco");
				for (fila = 2; fila <=27; fila++) {
					for (columna = 35; columna <= 116; columna++) {
						Posicion(columna, fila);
						Console.WriteLine(" ");
					}
				}
				
				Color("negro", "gris");
				for (fila = 2; fila <=27; fila++) {
					for (columna = 3; columna <= 33; columna++) {
						Posicion(columna, fila);
						Console.WriteLine(" ");
					}
				}
				
				if(idioma == "español")
				{
					Color("negro", "gris");
					Posicion(12,6);
					Console.WriteLine(" INFORMACION ");
					
					Posicion(12,9);
					Console.WriteLine("   USUARIOS   ");
					
					Posicion(12,12);
					Console.WriteLine("   NOTICIAS  ");
					
					Posicion(12,15);
					Console.WriteLine("    JUGAR    ");
					
					Posicion(12,18);
					Console.WriteLine(" DIAMOND-GPT ");
					
					Posicion(12,21);
					Console.WriteLine("CERRAR SESION");
					
					Color("amarillo", "negro");
					Posicion(5,27);
					Console.WriteLine(" C ");
					
					Color("negro", "gris");
					Posicion(10,27);
					Console.WriteLine("CUENTAS");
				}
				if(idioma == "english")
				{
					Color("negro", "gris");
					Posicion(12,6);
					Console.WriteLine(" INFORMATION ");
					
					Posicion(12,9);
					Console.WriteLine("    USERS    ");
					
					Posicion(12,12);
					Console.WriteLine("    NEWS     ");
					
					Posicion(12,15);
					Console.WriteLine("    PLAY     ");
					
					Posicion(12,18);
					Console.WriteLine(" DIAMOND-GPT ");
					
					Posicion(12,21);
					Console.WriteLine("   LOG OUT   ");
					
					Color("amarillo", "negro");
					Posicion(5,27);
					Console.WriteLine(" A ");
					
					Color("negro", "gris");
					Posicion(10,27);
					Console.WriteLine("ACCOUNTS");

				}
				
				if(nuevo == true)
				{
					Color("negro", "amarillo");
					Posicion(26,12);
					Console.WriteLine(" ! ");
				}
				
				if(opcion == 1)
				{
					
					ImprimirDatos("Datos.txt", usuario);
					
					if(puntosObtenidos > 0)
					{
						notificacion1 = false;
					}
					
					if(idioma == "español")
					{
						Color("blanco", "grisoscuro");
						Posicion(98,27);
						Console.Write(" BORRAR CUENTA ");
						Color("blanco", "rojo");
						Console.Write(" B ");
					}
					if(idioma == "english")
					{
						Color("blanco", "grisoscuro");
						Posicion(97,27);
						Console.Write(" DELETE ACCOUNT ");
						Color("blanco", "rojo");
						Console.Write(" D ");
					}
					
					
					Color("blanco", "verdeoscuro");
					Posicion(86,2);
					Console.WriteLine("                             ");
					Posicion(87,3);
					Console.WriteLine("                            ");
					Posicion(88,4);
					Console.WriteLine("                           ");
					
					Color("blanco", "negro");
					Posicion(87,2);
					Console.WriteLine("                             ");
					Posicion(88,3);
					Console.WriteLine("                            ");
					Posicion(89,4);
					Console.WriteLine("                           ");
					Posicion(93,3);
					Console.WriteLine(usuario);
					
					Color("blanco", "rojo");
					Posicion(39,9);
					Console.WriteLine("                ");
					Posicion(39,10);
					Console.WriteLine("                 ");
					Posicion(39,11);
					Console.WriteLine("                  ");
					
					if(idioma == "español")
					{
						Color("blanco", "negro");
						Posicion(38,9);
						Console.WriteLine("                ");
						Posicion(38,10);
						Console.WriteLine("    TU CORREO    ");
						Posicion(38,11);
						Console.WriteLine("                  ");
					}
					if(idioma == "english")
					{
						Color("blanco", "negro");
						Posicion(38,9);
						Console.WriteLine("                ");
						Posicion(38,10);
						Console.WriteLine("    YOUR EMAIL   ");
						Posicion(38,11);
						Console.WriteLine("                  ");
					}
					
					Color("blanco", "azul");
					Posicion(39,13);
					Console.WriteLine("                ");
					Posicion(39,14);
					Console.WriteLine("                 ");
					Posicion(39,15);
					Console.WriteLine("                  ");
					
					if(idioma == "español")
					{
						Color("blanco", "negro");
						Posicion(38,13);
						Console.WriteLine("                ");
						Posicion(38,14);
						Console.WriteLine("    TU NUMERO    ");
						Posicion(38,15);
						Console.WriteLine("                  ");
					}
					if(idioma == "english")
					{
						Color("blanco", "negro");
						Posicion(38,13);
						Console.WriteLine("                ");
						Posicion(38,14);
						Console.WriteLine("   YOUR NUMBER   ");
						Posicion(38,15);
						Console.WriteLine("                  ");
					}
					
					Color("blanco", "amarillooscuro");
					Posicion(39,17);
					Console.WriteLine("                ");
					Posicion(39,18);
					Console.WriteLine("                 ");
					Posicion(39,19);
					Console.WriteLine("                  ");
					
					if(idioma == "español")
					{
						Color("blanco", "negro");
						Posicion(38,17);
						Console.WriteLine("                ");
						Posicion(38,18);
						Console.WriteLine("      PUNTOS     ");
						Posicion(38,19);
						Console.WriteLine("                  ");
					}
					if(idioma == "english")
					{
						Color("blanco", "negro");
						Posicion(38,17);
						Console.WriteLine("                ");
						Posicion(38,18);
						Console.WriteLine("      POINTS     ");
						Posicion(38,19);
						Console.WriteLine("                  ");
					}
					
					Color("blanco", "cian");
					Posicion(39,21);
					Console.WriteLine("                ");
					Posicion(39,22);
					Console.WriteLine("                 ");
					Posicion(39,23);
					Console.WriteLine("                  ");
					
					if(idioma == "español")
					{
						Color("blanco", "negro");
						Posicion(38,21);
						Console.WriteLine("                ");
						Posicion(38,22);
						Console.WriteLine("      RANGO      ");
						Posicion(38,23);
						Console.WriteLine("                  ");
					}
					
					if(idioma == "english")
					{
						Color("blanco", "negro");
						Posicion(38,21);
						Console.WriteLine("                ");
						Posicion(38,22);
						Console.WriteLine("      RANGE      ");
						Posicion(38,23);
						Console.WriteLine("                  ");
					}
					
					if(notificacion1 == true)
					{
						Color("blanco", "cian");
						
						if(idioma == "español")
						{
							Posicion(89,21);
							Console.WriteLine("       NOTIFICACION       ");
						}
						if(idioma == "english")
						{
							Posicion(89,21);
							Console.WriteLine("       NOTIFICATION       ");
						}
						
						Posicion(89,22);
						Console.WriteLine("                           ");
						Posicion(89,23);
						Console.WriteLine("                           ");
						Posicion(89,24);
						Console.WriteLine("                           ");
						Posicion(89,25);
						Console.WriteLine("                           ");
						Posicion(89,26);
						Console.WriteLine("                           ");
						Posicion(89,27);
						Console.WriteLine("                           ");
						Color("blanco", "negro");
						Posicion(90,22);
						Console.WriteLine("                          ");
						
						if(idioma == "español")
						{
							Posicion(90,23);
							Console.WriteLine("   Consigue puntos en     ");
							Posicion(90,24);
							Console.WriteLine("   cualquier juego para   ");
							Posicion(90,25);
							Console.WriteLine("   subir tu rango y       ");
							Posicion(90,26);
							Console.WriteLine("   y ganar recompensas    ");
						}
						if(idioma == "english")
						{
							Posicion(90,23);
							Console.WriteLine("   Get points in          ");
							Posicion(90,24);
							Console.WriteLine("   any game to            ");
							Posicion(90,25);
							Console.WriteLine("   increase your rank and ");
							Posicion(90,26);
							Console.WriteLine("   and earn rewards       ");

						}
						Posicion(90,27);
						Console.WriteLine("                          ");
						
						notificacion1 = false;
					}
					
					switch(tecla.Key)
					{
						case ConsoleKey.UpArrow:
							
							Color("negro", "negro");
							fila = 5; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 6; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 7; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							break;
							
						case ConsoleKey.DownArrow:
							
							Color("negro", "negro");
							fila = 7; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 6; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 5; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							break;
							
						case ConsoleKey.B:
						case ConsoleKey.D:
							
							bool salir_borrar = false;
							
							Color("negro", "negro");
							for (fila = 19; fila <=27; fila++) {
								for (columna = 84; columna <= 115; columna++) {
									Posicion(columna, fila);
									Console.WriteLine(" ");
								}
							}
							
							Color("negro", "blanco");
							for (fila = 20; fila <=26; fila++) {
								for (columna = 86; columna <= 113; columna++) {
									Posicion(columna, fila);
									Console.WriteLine(" ");
								}
							}
							
							if(idioma == "español")
							{
								Color("rojo", "blanco");
								Posicion(90,21);
								Console.WriteLine("¿SEGURO QUE QUIERES");
								Posicion(90,22);
								Console.WriteLine("ELIMINAR TU CUENTA?");
							}
							if(idioma == "english")
							{
								Color("rojo", "blanco");
								Posicion(90,21);
								Console.WriteLine("ARE YOU SURE YOU WANT");
								Posicion(90,22);
								Console.WriteLine("TO DELETE YOUR ACCCOUNT?");
							}
							
							opcion = 98;
							
							while(salir_borrar == false)
							{
								
								Color("negro", "gris");
								for (fila = 24; fila <=26; fila++) {
									for (columna = 86; columna <= 99; columna++) {
										Posicion(columna, fila);
										Console.WriteLine(" ");
									}
								}
								
								Color("negro", "gris");
								for (fila = 24; fila <=26; fila++) {
									for (columna = 100; columna <= 113; columna++) {
										Posicion(columna, fila);
										Console.WriteLine(" ");
									}
								}
								
								if(idioma == "español")
								{
									Color("negro", "gris");
									Posicion(91,25);
									Console.WriteLine("BORRA");
									
									Color("negro", "gris");
									Posicion(103,25);
									Console.WriteLine("CANCELA");
								}
								if(idioma == "english")
								{
									Color("negro", "gris");
									Posicion(91,25);
									Console.WriteLine("DELETE");

									Color("negro", "gris");
									Posicion(103,25);
									Console.WriteLine("CANCEL");
								}
								
								if(opcion == 99)
								{
									Color("negro", "rojo");
									for (fila = 24; fila <=26; fila++) {
										for (columna = 86; columna <= 99; columna++) {
											Posicion(columna, fila);
											Console.WriteLine(" ");
										}
									}
									
									if(idioma == "español")
									{
										Color("blanco", "rojo");
										Posicion(91,25);
										Animacion("BORRA");
									}
									if(idioma == "english")
									{
										Color("blanco", "rojo");
										Posicion(91,25);
										Animacion("DELETE");
									}
									
								}
								if(opcion == 98)
								{
									Color("negro", "verdeoscuro");
									for (fila = 24; fila <=26; fila++) {
										for (columna = 100; columna <= 113; columna++) {
											Posicion(columna, fila);
											Console.WriteLine(" ");
										}
									}
									
									if(idioma == "español")
									{
										Color("blanco", "verdeoscuro");
										Posicion(103,25);
										Animacion("CANCELA");
									}
									if(idioma == "english")
									{
										Color("blanco", "verdeoscuro");
										Posicion(103,25);
										Animacion("CANCEL");
									}
								}
								
								Precionar();
								
								switch(tecla.Key)
								{
									case ConsoleKey.Escape:
										
										salir = true;
										break;
										
									case ConsoleKey.RightArrow:
										
										opcion = 98;
										
										break;
										
									case ConsoleKey.LeftArrow:

										opcion = 99;

										break;
										
									case ConsoleKey.Enter:
										
										if(opcion == 99)
										{
											BorrarCuenta("Datos.txt", usuario);
											salir = true;
										}
										else
										{
											Color("blanco", "blanco");
											for (fila = 19; fila <=27; fila++) {
												for (columna = 84; columna <= 115; columna++) {
													Posicion(columna, fila);
													Console.WriteLine(" ");
												}
											}
											if(idioma == "español")
											{
												Color("blanco", "grisoscuro");
												Posicion(98,27);
												Console.Write(" BORRAR CUENTA ");
												Color("blanco", "rojo");
												Console.Write(" B ");
											}
											if(idioma == "english")
											{
												Color("blanco", "grisoscuro");
												Posicion(97,27);
												Console.Write(" DELETE ACCOUNT ");
												Color("blanco", "rojo");
												Console.Write(" D ");
											}
											
										}
										salir_borrar = true;
										
										break;
								}
							}
							
							opcion = 1;
							break;
					}
					
					Color("negro", "negro");
					for (fila = 5; fila <=7; fila++) {
						for (columna = 3; columna <= 34; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
					
					if(idioma == "español")
					{
						Color("blanco", "negro");
						Posicion(12,6);
						Console.WriteLine(" INFORMACION ");
					}
					if(idioma == "english")
					{
						Color("blanco", "negro");
						Posicion(12,6);
						Console.WriteLine(" INFORMATION ");
					}
					
					Color("azul", "negro");
					fila = 5; //linea superior
					for( columna = 32; columna<=33; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("■");
					}
					Color("verde", "negro");
					fila = 6; //linea superior
					for( columna = 29; columna<=33; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("■");
					}
					Color("rojo", "negro");
					fila = 7; //linea superior
					for( columna = 31; columna<=33; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("■");
					}
				}
				if(opcion == 2)
				{
					switch(tecla.Key)
					{
						case ConsoleKey.UpArrow:
							
							Color("negro", "negro");
							fila = 8; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 9; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 10; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							break;
							
						case ConsoleKey.DownArrow:
							
							Color("negro", "negro");
							fila = 10; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 9; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 8; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							break;
							
						case ConsoleKey.Enter:
							
							Buscar();
							break;
					}
					
					Cursor(false);
					
					Color("negro", "negro");
					for (fila = 8; fila <=10; fila++) {
						for (columna = 3; columna <= 34; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
					
					Color("cian", "negro");
					fila = 8; //linea superior
					for( columna = 30; columna<=33; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("■");
					}
					Color("morado", "negro");
					fila = 9; //linea superior
					for( columna = 32; columna<=33; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("■");
					}
					Color("verde", "negro");
					fila = 10; //linea superior
					for( columna = 29; columna<=33; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("■");
					}
					
					Color("negro", "grisoscuro");
					for (fila = 3; fila <=26; fila++) {
						for (columna = 38; columna <= 113; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
					
					Color("negro", "blanco");
					for (fila = 5; fila <=23; fila++) {
						for (columna = 60; columna <= 90; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
					
					Color("grisoscuro", "blanco");
					Posicion(56,5);
					Console.WriteLine(" > ");
					
					Color("blanco", "grisoscuro");
					Posicion(65,6);
					Console.WriteLine("              ");
					
					Posicion(65,10);
					Console.WriteLine("                 ");
					
					Posicion(65,12);
					Console.WriteLine("          ");
					
					Posicion(65,14);
					Console.WriteLine("                   ");
					
					Posicion(65,16);
					Console.WriteLine("                ");
					
					Posicion(65,18);
					Console.WriteLine("             ");
					
					Posicion(65,20);
					Console.WriteLine("                  ");
					
					Posicion(65,22);
					Console.WriteLine("                ");
					
					
					string[] arteASCII =
					{
						" ╭━━━━━━━━━━━━━╮",
						" ┃   ══ ● ══   ┃",
						" ┃█████████████┃",
						" ┃█████████████┃",
						" ┃█████████████┃",
						" ┃█████████████┃",
						" ┃█████████████┃",
						" ┃█████████████┃",
						" ┃█████████████┃",
						" ┃█████████████┃",
						" ┃      ○      ┃",
						" ╰━━━━━━━━━━━━━╯"
					};

					// Imprimir el arte ASCII con Posicion()
					for (int i = 0; i < arteASCII.Length; i++)
					{
						Posicion(85, 8 + i);
						Console.WriteLine(arteASCII[i]);
					}
					
					if(idioma == "español")
					{
						Color("negro", "blanco");
						Posicion(54,26);
						Console.WriteLine(" Busca y ve la informacion de otra persona ");
						
						Color("blanco", "negro");
						Posicion(12,9);
						Console.WriteLine("   USUARIOS   ");
					}
					if(idioma == "english")
					{
						Color("negro", "blanco");
						Posicion(50,26);
						Console.WriteLine(" Search and view information about another person ");
						
						Color("blanco", "negro");
						Posicion(12,9);
						Console.WriteLine("    USERS     ");

					}
					
					
				}
				if(opcion == 3)
				{
					nuevo = false;
					
					switch(tecla.Key)
					{
						case ConsoleKey.UpArrow:
							
							Color("negro", "negro");
							fila = 11; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 12; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 13; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							break;
							
						case ConsoleKey.DownArrow:
							
							Color("negro", "negro");
							fila = 13; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 12; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 11; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							break;
					}
					
					Color("negro", "negro");
					for (fila = 11; fila <=13; fila++) {
						for (columna = 3; columna <= 34; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
					
					Color("verde", "negro");
					fila = 11; //linea superior
					for( columna = 32; columna<=33; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("■");
					}
					Color("rojo", "negro");
					fila = 12; //linea superior
					for( columna = 28; columna<=33; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("■");
					}
					Color("azul", "negro");
					fila = 13; //linea superior
					for( columna = 29; columna<=33; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("■");
					}
					
					Color("blanco", "morado");
					Posicion(86,2);
					Console.WriteLine("                             ");
					Posicion(87,3);
					Console.WriteLine("                            ");
					Posicion(88,4);
					Console.WriteLine("                           ");
					
					if(idioma == "español")
					{
						Color("blanco", "negro");
						Posicion(12,12);
						Console.WriteLine("   NOTICIAS   ");
						
						Color("blanco", "negro");
						Posicion(87,2);
						Console.WriteLine("                             ");
						Posicion(88,3);
						Console.WriteLine("            NUEVO           ");
						Posicion(89,4);
						Console.WriteLine("                           ");
						
						Color("negro", "gris");
						Posicion(40,3);
						Console.WriteLine("█                                  █");
						Posicion(40,4);
						Console.WriteLine("█  Se agrego un juego al programa  █");
						Posicion(40,5);
						Console.WriteLine("█                                  █");
						
						Posicion(40,7);
						Console.WriteLine("█                                           █");
						Posicion(40,8);
						Console.WriteLine("█  Opcion de eliminar la cuenta disponible  █");
						Posicion(40,9);
						Console.WriteLine("█                                           █");
						
						Posicion(40,11);
						Console.WriteLine("█                                            █");
						Posicion(40,12);
						Console.WriteLine("█  Diamond-GPT ahora tiene una mejor logica  █");
						Posicion(40,13);
						Console.WriteLine("█                                            █");
						
						Posicion(40,15);
						Console.WriteLine("█                                                          █");
						Posicion(40,16);
						Console.WriteLine("█  Se corrigieron errores de compativilidad en windows 11  █");
						Posicion(40,17);
						Console.WriteLine("█                                                          █");
						
						Posicion(40,19);
						Console.WriteLine("█                                                    █");
						Posicion(40,20);
						Console.WriteLine("█  Ahora puedes buscar los datos de otras personas   █");
						Posicion(40,21);
						Console.WriteLine("█  Y comparar quien tiene mas puntos!                █");
						Posicion(40,22);
						Console.WriteLine("█                                                    █");
						
						Posicion(40,24);
						Console.WriteLine("█                                                    █");
						Posicion(40,25);
						Console.WriteLine("█  El programa esta en su version ingles completada  █");
						Posicion(40,26);
						Console.WriteLine("█                                                    █");
					}
					if(idioma == "english")
					{
						Color("blanco", "negro");
						Posicion(12,12);
						Console.WriteLine("   NEWS   ");

						Color("blanco", "negro");
						Posicion(87,2);
						Console.WriteLine("                             ");
						Posicion(88,3);
						Console.WriteLine("            NEW           ");
						Posicion(89,4);
						Console.WriteLine("                           ");

						Color("negro", "gris");
						Posicion(40,3);
						Console.WriteLine("█                                        █");
						Posicion(40,4);  
						Console.WriteLine("█  A game has been added to the program  █");
						Posicion(40,5);
						Console.WriteLine("█                                        █");

						Posicion(40,7);
						Console.WriteLine("█                                     █");
						Posicion(40,8);
						Console.WriteLine("█  Account deletion option available  █");
						Posicion(40,9);
						Console.WriteLine("█                                     █");

						Posicion(40,11);
						Console.WriteLine("█                                    █");
						Posicion(40,12);
						Console.WriteLine("█  Diamond-GPT now has better logic  █");
						Posicion(40,13);
						Console.WriteLine("█                                    █");

						Posicion(40,15);
						Console.WriteLine("█                                            █");
						Posicion(40,16);
						Console.WriteLine("█  Compatibility issues fixed in Windows 11  █");
						Posicion(40,17);
						Console.WriteLine("█                                            █");

						Posicion(40,19);
						Console.WriteLine("█                                               █");
						Posicion(40,20);
						Console.WriteLine("█  You can now search for other people's data   █");
						Posicion(40,21);
						Console.WriteLine("█  And compare who has more points!             █");
						Posicion(40,22);
						Console.WriteLine("█                                               █");

						Posicion(40,24);
						Console.WriteLine("█                                                  █");
						Posicion(40,25);
						Console.WriteLine("█  The program is now fully translated to English  █");
						Posicion(40,26);
						Console.WriteLine("█                                                  █");
					}
				}
				if(opcion == 4)
				{
					switch(tecla.Key)
					{
						case ConsoleKey.UpArrow:
							
							Color("negro", "negro");
							fila = 14; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 15; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 16; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							break;
							
						case ConsoleKey.DownArrow:
							
							Color("negro", "negro");
							fila = 16; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 15; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 14; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							break;
							
						case ConsoleKey.Enter:
							
							Juego1();
							break;
					}
					
					Color("negro", "negro");
					for (fila = 14; fila <=16; fila++) {
						for (columna = 3; columna <= 34; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
					
					Color("amarillo", "negro");
					fila = 14; //linea superior
					for( columna = 29; columna<=33; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("■");
					}
					Color("verde", "negro");
					fila = 15; //linea superior
					for( columna = 31; columna<=33; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("■");
					}
					Color("morado", "negro");
					fila = 16; //linea superior
					for( columna = 27; columna<=33; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("■");
					}
					
					Color("blanco", "verde");
					Posicion(86,2);
					Console.WriteLine("                             ");
					Posicion(87,3);
					Console.WriteLine("                            ");
					Posicion(88,4);
					Console.WriteLine("                           ");
					
					if(idioma == "español")
					{
						Color("blanco", "negro");
						Posicion(12,15);
						Console.WriteLine("    JUGAR    ");
						
						Posicion(87,2);
						Console.WriteLine("                             ");
						Posicion(88,3);
						Console.WriteLine("       TRES EN RAYA         ");
						Posicion(89,4);
						Console.WriteLine("                           ");
					}
					if(idioma == "english")
					{
						Color("blanco", "negro");
						Posicion(12,15);
						Console.WriteLine("    PLAY     ");
						
						Posicion(87,2);
						Console.WriteLine("                             ");
						Posicion(88,3);
						Console.WriteLine("        TIC-TAC-TOE         ");
						Posicion(89,4);
						Console.WriteLine("                           ");
					}
					
					Color("blanco", "gris");
					Posicion(36,25);
					Console.WriteLine("                                                                                ");
					Posicion(36,26);
					Console.WriteLine("                                                                                ");
					Posicion(36,27);
					Console.WriteLine("                                                                                ");
					
					if(idioma == "español")
					{
						string[] nota = {
							"Nota: las teclas \"A\", \"S\", \"D\", \"W\", \"Space\" estan disponibles",
							"Nota: preciona la tecla \"R\" para reiniciar el juego.",
							"Nota: El jugador que gane comenzara la ronda siquiente."
						};

						int indice;
						Random rnd = new Random();
						List<int> mensajesMostrados = new List<int>();
						do
						{
							indice = rnd.Next(0, nota.Length);
						} while (mensajesMostrados.Contains(indice)); // Verifica si el mensaje ya ha sido mostrado

						mensajesMostrados.Add(indice); // Agrega el índice del mensaje mostrado a la lista

						Color("negro", "gris");
						Posicion(37,26);
						Console.Write(nota[indice]);
					}
					if(idioma == "english")
					{
						string[] note = {
							"Note: the keys \"A\", \"S\", \"D\", \"W\", \"Space\" are available",
							"Note: press the key \"R\" to restart the game.",
							"Note: The player who wins will start the next round."
						};

						int index;
						Random rnd = new Random();
						List<int> shownMessages = new List<int>();
						do
						{
							index = rnd.Next(0, note.Length);
						} while (shownMessages.Contains(index)); // Comprueba si el mensaje ya ha sido mostrado

						shownMessages.Add(index); // Agrega el índice del mensaje mostrado a la lista

						Color("negro", "gris");
						Posicion(37,26);
						Console.Write(note[index]);

					}
					
					Color("blanco", "gris");
					columna = 69; //lado iskierdo
					for( fila = 8; fila<=21; fila=fila+1 ){
						Posicion(columna, fila);Console.WriteLine(" ");
					}
					
					columna = 81; //lado iskierdo
					for( fila = 8; fila<=21; fila=fila+1 ){
						Posicion(columna, fila);Console.WriteLine(" ");
					}
					
					fila = 12;
					for( columna = 60; columna<=90; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("▄");
					}
					
					fila = 17;
					for( columna = 60; columna<=90; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("▄");
					}
				}
				if(opcion == 5)
				{
					switch(tecla.Key)
					{
						case ConsoleKey.UpArrow:
							
							Color("negro", "negro");
							fila = 17; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 18; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 19; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							break;
							
						case ConsoleKey.DownArrow:
							
							Color("negro", "negro");
							fila = 19; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 18; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 17; //linea superior
							for( columna = 3; columna<=34; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							break;
							
						case ConsoleKey.Enter:
							
							GPT();
							break;
					}
					
					string[] arteASCII =
					{
						" ______     .-./`)    ____    ,---.    ,---.    ,-----.    ,---.   .--. ______    ",
						"|    _ `''. \\ .-.') .'  __ `. |    \\  /    |  .'  .-,  '.  |    \\  |  ||    _ \\.",
						"| _ | ) _  \\/ `-' \\/   '  \\  \\|  ,  \\/  ,  | / ,-.|  \\ _ \\ |  ,  \\ |  || _ | ) _ ",
						"|( ''_'  ) | `-'`\" |___|  /  ||  |\\_   /|  |;  \\  '_ /  | :|  |\\_ \\|  ||( ''_'  ) ",
						"| . (_) `. | .---.    _.-`   ||  _( )_/ |  ||  _`,/ \\ _/  ||  _( )_\\  || . (_) `. ",
						"|(_    ._) ' |   | .'   _    || (_ o _) |  |: (  '\\_/ \\   ;| (_ o _)  ||(_    ._) ",
						"|  (_.\\.' /  |   | |  _( )_  ||  (_,_)  |  | \\ `\"/  \\  ) / |  (_,_\\  ||  (_.\\.' /",
						"|       .'   |   | \\ (_ o _) /|  |      |  |  '. \\_/``\".'  |  |    |  ||       .' ",
						"'-----'`     '---'  '.(_,_).' '--'      '--'    '-----'    '--'    '--''-----'`   ",
						"                       .-_'''-.   .-------. ,---------.              ",
						"                      '_( )_   \\  \\  _(`)_ \\\\          \\  ",
						"                     |(_ o _)|  ' | (_ o._)| `--.  ,---'      ",
						"                     . (_,_)/___| |  (_,_) /    |   \\        ",
						"                     |  |  .-----.|   '-.-'     :_ _:        ",
						"                     '  \\  '-   .'|   |         (_I_)       ",
						"                      \\  `-'`   | |   |        (_(=)_)      ",
						"                       \\        / /   )         (_I_)        ",
						"                        `'-...-'  `---'         '---'          "
					};
					
					

					// Imprimir el arte ASCII con Posicion()
					for (int i = 0; i < arteASCII.Length; i++)
					{
						Color("negro", "blanco");
						Posicion(35, 6 + i);
						Console.WriteLine(arteASCII[i]);
					}
					
					if(idioma == "español")
					{
						Color("blanco", "negro");
						Posicion(35, 25);
						Console.WriteLine("                                                                                  ");
						Posicion(35, 26);
						Console.WriteLine("                       Preciona ENTER para ir al chat                             ");
						Posicion(35, 27);
						Console.WriteLine("                                                                                  ");
					}
					if(idioma == "english")
					{
						Color("blanco", "negro");
						Posicion(35, 25);
						Console.WriteLine("                                                                                  ");
						Posicion(35, 26);
						Console.WriteLine("                       Press ENTER to go to the chat                             ");
						Posicion(35, 27);
						Console.WriteLine("                                                                                  ");

					}
					
					
					Color("negro", "negro");
					for (fila = 17; fila <=19; fila++) {
						for (columna = 3; columna <= 34; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
					
					Color("verde", "negro");
					fila = 17; //linea superior
					for( columna = 28; columna<=33; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("■");
					}
					Color("rojo", "negro");
					fila = 18; //linea superior
					for( columna = 29; columna<=33; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("■");
					}
					Color("azul", "negro");
					fila = 19; //linea superior
					for( columna = 32; columna<=33; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("■");
					}
					
					Color("blanco", "negro");
					Posicion(12,18);
					Console.WriteLine(" DIAMOND-GPT ");
					
				}
				if(opcion == 6)
				{
					switch(tecla.Key)
					{
						case ConsoleKey.UpArrow:
							
							Color("negro", "rojooscuro");
							fila = 20; //linea superior
							for( columna = 3; columna<=33; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 21; //linea superior
							for( columna = 3; columna<=33; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 22; //linea superior
							for( columna = 3; columna<=33; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							break;
							
						case ConsoleKey.DownArrow:
							
							Color("negro", "rojooscuro");
							fila = 22; //linea superior
							for( columna = 3; columna<=33; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 21; //linea superior
							for( columna = 3; columna<=33; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							Tiempo(15);
							fila = 20; //linea superior
							for( columna = 3; columna<=33; columna=columna+1 ){
								Posicion(columna, fila);Console.WriteLine(" ");
							}
							break;
					}
					
					if(idioma == "español")
					{
						string[] arteASCII =
						{
							" ██████ ███████ ██████  ██████   █████  ██████  ",
							"██      ██      ██   ██ ██   ██ ██   ██ ██   ██ ",
							"██      █████   ██████  ██████  ███████ ██████  ",
							"██      ██      ██   ██ ██   ██ ██   ██ ██   ██ ",
							" ██████ ███████ ██   ██ ██   ██ ██   ██ ██   ██ ",
							"                                                ",
							"                                                ",
							"███████ ███████ ███████ ██  ██████  ███    ██   ",
							"██      ██      ██      ██ ██    ██ ████   ██   ",
							"███████ █████   ███████ ██ ██    ██ ██ ██  ██   ",
							"     ██ ██           ██ ██ ██    ██ ██  ██ ██   ",
							"███████ ███████ ███████ ██  ██████  ██   ████   "
						};

						// Imprimir el arte ASCII con Posicion()
						for (int i = 0; i < arteASCII.Length; i++)
						{
							Color("gris", "blanco");
							Posicion(50, 8 + i);
							Console.WriteLine(arteASCII[i]);
						}
					}
					if(idioma == "english")
					{
						string[] arteASCII2 =
						{
							"██       ██████   ██████       ██████  ██    ██ ████████ ",
							"██      ██    ██ ██           ██    ██ ██    ██    ██    ",
							"██      ██    ██ ██   ███     ██    ██ ██    ██    ██    ",
							"██      ██    ██ ██    ██     ██    ██ ██    ██    ██    ",
							"███████  ██████   ██████       ██████   ██████     ██   "
						};

						// Imprimir el arte ASCII con Posicion()
						for (int i = 0; i < arteASCII2.Length; i++)
						{
							Color("gris", "blanco");
							Posicion(47, 10 + i);
							Console.WriteLine(arteASCII2[i]);
						}
					}
					
					Color("negro", "rojooscuro");
					for (fila = 20; fila <=22; fila++) {
						for (columna = 3; columna <= 33; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
					
					Color("negro", "rojooscuro");
					fila = 20; //linea superior
					for( columna = 32; columna<=33; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("■");
					}
					fila = 21; //linea superior
					for( columna = 29; columna<=33; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("■");
					}
					fila = 22; //linea superior
					for( columna = 29; columna<=33; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("■");
					}
					
					if(idioma == "español")
					{
						Color("blanco", "rojooscuro");
						Posicion(12,21);
						Console.WriteLine("CERRAR SESION");
					}
					if(idioma == "english")
					{
						Color("blanco", "rojooscuro");
						Posicion(12,21);
						Console.WriteLine("   LOG OUT   ");

					}
				}
				if(opcion == 7)
				{
					Color("negro", "amarillo");
					Posicion(5,27);
					Animacion(" - ");
					
					Color("negro", "gris");
					Posicion(10,27);
					Animacion("     ");
					
					Color("blanco", "rojo");
					Posicion(86,2);
					Console.WriteLine("                             ");
					Posicion(87,3);
					Console.WriteLine("                            ");
					Posicion(88,4);
					Console.WriteLine("                           ");
					
					if(idioma == "español")
					{
						Color("blanco", "negro");
						Posicion(87,2);
						Console.WriteLine("                             ");
						Posicion(88,3);
						Console.WriteLine("     LISTA DE CUENTAS       ");
						Posicion(89,4);
						Console.WriteLine("                           ");
						
						Color("negro", "gris");
						Posicion(10,27);
						Animacion("CUENTAS");
					}
					if(idioma == "english")
					{
						Color("blanco", "negro");
						Posicion(87,2);
						Console.WriteLine("                             ");
						Posicion(88,3);
						Console.WriteLine("      LIST OF ACCOUNTS      ");
						Posicion(89,4);
						Console.WriteLine("                           ");
						
						Color("negro", "gris");
						Posicion(10,27);
						Animacion("ACCOUNTS");
					}
					
					Cuentas();
				}
				
				Precionar();
				
				switch(tecla.Key)
				{
					case ConsoleKey.UpArrow:
						
						opcion = Math.Max(opcion - 1, 1);
						break;
						
					case ConsoleKey.DownArrow:
						
						opcion = Math.Min(opcion + 1, 7);
						break;
						
					case ConsoleKey.A:
					case ConsoleKey.C:
						
						opcion = 7;
						break;
				}
				
				if(tecla.Key == ConsoleKey.Enter)
				{
					switch(opcion)
					{
						case 6:

							salir = true;
							break;
							
						case 7:
							
							break;
					}

				}
				
			}while(salir == false);
			
			opcion = 1;
			pregunta = "";
			
			Color("negro", "negro");
			Reset();
			
			salir = false;
			
			Color("negro", "blanco");
			for (fila = 3; fila <=19; fila++) {
				for (columna = 36; columna <= 84; columna++) {
					Posicion(columna, fila);
					Console.WriteLine(" ");
				}
			}
		}
		
		static void Buscar()
		{
			Color("negro", "blanco");
			for (fila = 3; fila <=26; fila++) {
				for (columna = 38; columna <= 113; columna++) {
					Posicion(columna, fila);
					Console.WriteLine(" ");
				}
			}
			
			Color("negro", "grisoscuro");
			for (fila = 3; fila <=26; fila++) {
				for (columna = 40; columna <= 111; columna++) {
					Posicion(columna, fila);
					Console.WriteLine(" ");
				}
			}
			
			Color("negro", "gris");
			for (fila = 4; fila <=25; fila++) {
				for (columna = 42; columna <= 109; columna++) {
					Posicion(columna, fila);
					Console.WriteLine(" ");
				}
			}
			
			Color("negro", "blanco");
			for (fila = 7; fila <=24; fila++) {
				for (columna = 55; columna <= 95; columna++) {
					Posicion(columna, fila);
					Console.WriteLine(" ");
				}
			}
			
			Color("blanco", "negro");
			Posicion(55,7);
			Console.WriteLine("█ █ █ █ █ █ █ █ █ █ █ █ █ █ █ █ █ █ █ █ █");
			
			Color("gris", "blanco");
			Posicion(55,9);
			Console.WriteLine("─────────────────────────────────────────");
			Posicion(55,11);
			Console.WriteLine("─────────────────────────────────────────");
			Posicion(55,13);
			Console.WriteLine("─────────────────────────────────────────");
			Posicion(55,15);
			Console.WriteLine("─────────────────────────────────────────");
			Posicion(55,17);
			Console.WriteLine("─────────────────────────────────────────");
			Posicion(55,19);
			Console.WriteLine("─────────────────────────────────────────");
			Posicion(55,21);
			Console.WriteLine("─────────────────────────────────────────");
			Posicion(55,23);
			Console.WriteLine("─────────────────────────────────────────");
			
			if(idioma == "español")
			{
				Color("negro", "gris");
				Posicion(55,5);
				Console.WriteLine("Buscar a                                 ");
			}
			if(idioma == "english")
			{
				Color("negro", "gris");
				Posicion(55,5);
				Console.WriteLine("Search for                               ");
			}
			
			do
			{
				usuarioBuscar = "";
				
				Color("negro", "gris");
				Posicion(55,6);
				Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");
				
				Cursor(true);
				
				if(idioma == "español")
				{
					Posicion(64,5);
				}
				if(idioma == "english")
				{
					Posicion(66,5);
				}
				while (true)
				{
					ConsoleKeyInfo keyInfo = Console.ReadKey(true); // Lee una tecla presionada sin mostrarla en la pantalla
					
					if (keyInfo.Key == ConsoleKey.Escape || keyInfo.Key == ConsoleKey.Enter && usuarioBuscar != "") // Si se presiona la tecla Enter, salir del bucle
					{
						Cursor(false);
						
						if(Existe(usuarioBuscar) && usuarioBuscar != "")
						{
							Color("negro", "gris");
							Posicion(55,5);
							Console.WriteLine("Buscar a                                   ");
							
							Color("negro", "blanco");
							for (fila = 7; fila <=24; fila++) {
								for (columna = 55; columna <= 95; columna++) {
									Posicion(columna, fila);
									Console.WriteLine(" ");
								}
							}
							
							Color("blanco", "negro");
							Posicion(55,7);
							Console.WriteLine("█ █ █ █ █ █ █ █ █ █ █ █ █ █ █ █ █ █ █ █ █");
							
							Color("gris", "blanco");
							Posicion(55,9);
							Console.WriteLine("─────────────────────────────────────────");
							Posicion(55,11);
							Console.WriteLine("─────────────────────────────────────────");
							Posicion(55,13);
							Console.WriteLine("─────────────────────────────────────────");
							Posicion(55,15);
							Console.WriteLine("─────────────────────────────────────────");
							Posicion(55,17);
							Console.WriteLine("─────────────────────────────────────────");
							Posicion(55,19);
							Console.WriteLine("─────────────────────────────────────────");
							Posicion(55,21);
							Console.WriteLine("─────────────────────────────────────────");
							Posicion(55,23);
							Console.WriteLine("─────────────────────────────────────────");
							
							Azul("gris");
							Posicion(55,6);
							Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");
							Tiempo(50);
							
							BuscarUsuario("Datos.txt", usuarioBuscar);
						}
						else
						{
							Color("negro", "gris");
							Posicion(55,5);
							Console.Write("Buscar a                                 ");
							
							Color("rojo", "gris");
							Console.Write("!");
							
							Posicion(55,6);
							Console.WriteLine("¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯");
							Tiempo(50);
						}
						if(keyInfo.Key == ConsoleKey.Escape)
						{
							salir = true;
							break;
						}
						Cursor(false);
						break;
					}
					else if (keyInfo.Key == ConsoleKey.Backspace && usuarioBuscar.Length > 0) // Si se presiona la tecla Retroceso, eliminar un carácter del nombre de usuario
					{
						usuarioBuscar = usuarioBuscar.Substring(0, usuarioBuscar.Length - 1);
						Console.Write("\b \b"); // Borra el carácter de la pantalla
					}
					else if (usuarioBuscar.Length < 26) // Si el límite de caracteres no ha sido alcanzado
					{
						// Permitir letras y espacios, pero no números ni caracteres especiales
						if (char.IsLetterOrDigit(keyInfo.KeyChar) || keyInfo.KeyChar == ' ')
						{
							Azul("gris");
							usuarioBuscar += keyInfo.KeyChar; // Agregar la tecla al nombre de usuario
							Console.Write(keyInfo.KeyChar); // Mostrar la tecla en la pantalla
						}
					}
					
				}
				
			}while(salir == false);
			salir = false;
		}
		
		static void tuiInicio()
		{
			
			int fila, columna;
			
			Color("negro", "blanco");
			fila = 1; //linea superior
			for( columna = 2; columna<=116; columna=columna+1 ){
				Posicion(columna, fila);Console.WriteLine("▀");
			}
			
			fila = 28; //linea inferior
			for( columna = 34; columna<=116; columna=columna+1 ){
				Posicion(columna, fila);Console.WriteLine("▄");
			}
			
			columna = 34; //lado separador
			for( fila = 1; fila<=28; fila=fila+1 ){
				Posicion(columna, fila);Console.WriteLine("█");
			}
			
			columna = 117; //lado derecho
			for( fila = 1; fila<=28; fila=fila+1 ){
				Posicion(columna, fila);Console.WriteLine("█");
			}
			
			Color("negro", "gris");
			fila = 1; //linea superior
			for( columna = 2; columna<=33; columna=columna+1 ){
				Posicion(columna, fila);Console.WriteLine("▀");
			}
			
			fila = 28; //linea inferior
			for( columna = 2; columna<=33; columna=columna+1 ){
				Posicion(columna, fila);Console.WriteLine("▄");
			}
			
			columna = 2; //lado iskierdo
			for( fila = 1; fila<=28; fila=fila+1 ){
				Posicion(columna, fila);Console.WriteLine("█");
			}
		}

		
		static bool Existe(string usuarioIngresado)
		{
			string archivo = "Datos.txt";

			// Leer todas las líneas del archivo
			string[] nombresUsuarios = File.ReadAllLines(archivo);

			// Verificar si el usuario ingresado está en la lista de nombres de usuarios
			foreach (string nombreUsuario in nombresUsuarios)
			{
				if (nombreUsuario.Trim() == usuarioIngresado.Trim()) // Comparación de nombres de usuario (ignorando espacios en blanco)
				{
					return true; // Usuario encontrado
				}
			}

			return false; // Usuario no encontrado
		}
		
		static void Sesion()
		{
			//cargar los datos del archivo
			string[] datosCargados = Cargar("Datos.txt");
			
			Color("NEGRO", "blanco");
			fila = 2; //linea superior
			for( columna = 35; columna<=85; columna=columna+1 ){
				Posicion(columna, fila);Console.WriteLine("▄");
			}
			
			fila = 26; //linea inferior
			for( columna = 35; columna<=85; columna=columna+1 ){
				Posicion(columna, fila);Console.WriteLine("▀");
			}
			
			columna = 35; //lado iskierdo
			for( fila = 3; fila<=25; fila=fila+1 ){
				Posicion(columna, fila);Console.WriteLine("█");
			}
			
			columna = 85; //lado derecho
			for( fila = 3; fila<=25; fila=fila+1 ){
				Posicion(columna, fila);Console.WriteLine("█");
			}
			
			
			Color("blanco", "blanco");
			columna = 34; //lado iskierdo
			for( fila = 2; fila<=26; fila=fila+1 ){
				Posicion(columna, fila);Console.WriteLine("█");
			}
			
			columna = 86; //lado derecho
			for( fila = 2; fila<=26; fila=fila+1 ){
				Posicion(columna, fila);Console.WriteLine("█");
			}
			
			//icons
			
			Color("negro", "azuloscuro");
			Posicion(87,4);
			Console.WriteLine("        ");
			Color("negro", "azul");
			Posicion(87,6);
			Console.WriteLine("     ");
			Color("negro", "azuloscuro");
			Posicion(87,8);
			Console.WriteLine("          ");
			Color("negro", "azul");
			Posicion(87,10);
			Console.WriteLine("         ");
			Color("negro", "azuloscuro");
			Posicion(87,12);
			Console.WriteLine("              ");
			Color("negro", "azul");
			Posicion(87,14);
			Console.WriteLine("      ");
			Color("negro", "azuloscuro");
			Posicion(87,16);
			Console.WriteLine("          ");
			Color("negro", "azul");
			Posicion(87,18);
			Console.WriteLine("              ");
			Color("negro", "azuloscuro");
			Posicion(87,20);
			Console.WriteLine("       ");
			Color("negro", "azul");
			Posicion(87,22);
			Console.WriteLine("          ");
			Color("negro", "azuloscuro");
			Posicion(87,24);
			Console.WriteLine("     ");
			
			
			Color("negro", "azuloscuro");
			Posicion(26,4);
			Console.WriteLine("        ");
			Color("negro", "azul");
			Posicion(29,6);
			Console.WriteLine("     ");
			Color("negro", "azuloscuro");
			Posicion(24,8);
			Console.WriteLine("          ");
			Color("negro", "azul");
			Posicion(25,10);
			Console.WriteLine("         ");
			Color("negro", "azuloscuro");
			Posicion(20,12);
			Console.WriteLine("              ");
			Color("negro", "azul");
			Posicion(28,14);
			Console.WriteLine("      ");
			Color("negro", "azuloscuro");
			Posicion(24,16);
			Console.WriteLine("          ");
			Color("negro", "azul");
			Posicion(20,18);
			Console.WriteLine("              ");
			Color("negro", "azuloscuro");
			Posicion(27,20);
			Console.WriteLine("       ");
			Color("negro", "azul");
			Posicion(24,22);
			Console.WriteLine("          ");
			Color("negro", "azuloscuro");
			Posicion(29,24);
			Console.WriteLine("     ");
			
			if(idioma == "español")
			{
				Color("blanco", "NEGRO");
				Posicion(36,4);
				Console.WriteLine("         INGRESA A TU CUENTA DE DIAMOND          ");
			}
			if(idioma == "english")
			{
				Color("blanco", "NEGRO");
				Posicion(36,4);
				Console.WriteLine("         LOG IN TO YOUR DIAMOND ACCOUNT          ");
			}
			
			if(idioma == "español")
			{
				Color("negro", "blanco");
				Posicion(39,11);
				Console.WriteLine("   USUARIO: ");
				
				Posicion(39,14);
				Console.WriteLine("   CONTRASEÑA: ");
			}
			if(idioma == "english")
			{
				Color("negro", "blanco");
				Posicion(39,11);
				Console.WriteLine("   USER: ");
				
				Posicion(39,14);
				Console.WriteLine("   PASS: ");
			}
			
			Color("blanco", "gris");
			for (fila = 20; fila <= 22; fila++) {
				for (columna = 36; columna <= 84; columna++) {
					Posicion(columna, fila);
					Console.WriteLine(" ");
				}
			}
			Color("blanco", "gris");
			for (fila = 23; fila <= 25; fila++) {
				for (columna = 36; columna <= 84; columna++) {
					Posicion(columna, fila);
					Console.WriteLine(" ");
				}
			}
			Color("grisoscuro", "blanco");
			fila = 19; //borrar linea inferior
			for( columna = 36; columna<=84; columna=columna+1 ){
				Posicion(columna, fila);Console.WriteLine("_");
			}
			
			if(idioma == "español")
			{
				Color("negro", "gris");
				Posicion(53,21);
				Console.WriteLine("INICIAR SESION");
				
				Color("negro", "gris");
				Posicion(54,24);
				Console.WriteLine("REGISTRARSE");
				
				Color("negro", "amarillo");
				Posicion(114,28);
				Console.WriteLine(" ? ");
				
				Color("amarillo", "negro");
				Posicion(104,28);
				Console.WriteLine("ACERCA DE");
				
				Color("negro", "amarillo");
				Posicion(2,28);
				Console.WriteLine(" ☻ ");
				
				if(opcion != 6)
				{
					Color("amarillo", "negro");
					Posicion(6,28);
					Console.WriteLine("IDIOMA                        ");
				}
			}
			if(idioma == "english")
			{
				Color("negro", "gris");
				Posicion(53,21);
				Console.WriteLine("    LOG IN    ");
				
				Color("negro", "gris");
				Posicion(55,24);
				Console.WriteLine("  SING UP  ");
				
				Color("negro", "amarillo");
				Posicion(114,28);
				Console.WriteLine(" ? ");
				
				Color("amarillo", "negro");
				Posicion(104,28);
				Console.WriteLine("   ABOUT ");
				
				Color("negro", "amarillo");
				Posicion(2,28);
				Console.WriteLine(" ☻ ");
				
				if(opcion != 6)
				{
					Color("amarillo", "negro");
					Posicion(6,28);
					Console.WriteLine("LANGUAGE                        ");
				}
			}
			
			
			if(opcion == 1)
			{
				ColorRandomBlanco();
				Posicion(37,11);
				Console.WriteLine(">");
				Tiempo(20);
				
				ColorRandomBlanco();
				Posicion(37,11);
				Console.WriteLine(" >");
				Tiempo(20);
				
				if(idioma == "español")
				{
					Color("negro", "blanco");
					Posicion(37,11);
					Console.WriteLine("  >  USUARIO: ");
				}
				if(idioma == "english")
				{
					Color("negro", "blanco");
					Posicion(37,11);
					Console.WriteLine("  >  USER: ");
				}
				
				fila = 15; //borrar linea inferior
				for( columna = 42; columna<=78; columna=columna+1 ){
					Posicion(columna, fila);Console.WriteLine(" ");
				}
				
				if(tecla.Key == ConsoleKey.Enter)
				{
					if(idioma == "español")
					{
						Color("negro", "blanco");
						Posicion(39,11);
						Console.WriteLine("   USUARIO: ");
						
						
						Posicion(51,11);
						Console.WriteLine("                                ");
						Posicion(39,14);
						Console.WriteLine(" ");
					}
					if(idioma == "english")
					{
						Color("negro", "blanco");
						Posicion(39,11);
						Console.WriteLine("   USER: ");
						
						
						Posicion(48,11);
						Console.WriteLine("                                ");
						Posicion(39,14);
						Console.WriteLine(" ");
					}
					
					usuario = "";
					Cursor(true);
					
					Posicion(42,12);
					Console.WriteLine("-----------------------------------");
					
					if(idioma == "español")
					{
						Posicion(51,11);
						while (true)
						{
							ConsoleKeyInfo keyInfo = Console.ReadKey(true); // Lee una tecla presionada sin mostrarla en la pantalla

							if(keyInfo.Key == ConsoleKey.Escape || keyInfo.Key == ConsoleKey.Enter && usuario != "") // Si se presiona la tecla Enter, salir del bucle
							{
								if(keyInfo.Key == ConsoleKey.Escape)
								{
									usuario = "";
									Posicion(51,11);
									Console.WriteLine("                                ");
								}
								
								break;
							}
							else if (keyInfo.Key == ConsoleKey.Backspace && usuario.Length > 0) // Si se presiona la tecla Retroceso, eliminar un carácter del nombre de usuario
							{
								usuario = usuario.Substring(0, usuario.Length - 1);
								Console.Write("\b \b"); // Borra el carácter de la pantalla
							}
							else if (usuario.Length < 21) // Si el límite de caracteres no ha sido alcanzado
							{
								// Permitir letras y espacios, pero no números ni caracteres especiales
								if (char.IsLetterOrDigit(keyInfo.KeyChar) || keyInfo.KeyChar == ' ')
								{
									usuario += keyInfo.KeyChar; // Agregar la tecla al nombre de usuario
									Console.Write(keyInfo.KeyChar); // Mostrar la tecla en la pantalla
								}
							}
						}
						
						Cursor(false);
						
						Color("blanco", "blanco");
						fila = 12; //borrar linea inferior
						for( columna = 42; columna<=78; columna=columna+1 ){
							Posicion(columna, fila);Console.WriteLine(" ");
						}
						
						Color("negro", "blanco");
						Posicion(39,11);
						Console.WriteLine(">  USUARIO: ");
						
						if(!Existe(usuario) && usuario != "")
						{
							Posicion(51,11);
							Console.Write(usuario);
							Color("amarillooscuro", "blanco");
							Console.Write(" !");
						}
						if(Existe(usuario) && usuario != "")
						{
							opcion = 2;
						}
						if(contraseña != "" && usuario != "")
						{
							opcion = 3;
						}
						
					}
					if(idioma == "english")
					{
						Posicion(48,11);
						while (true)
						{
							ConsoleKeyInfo keyInfo = Console.ReadKey(true); // Lee una tecla presionada sin mostrarla en la pantalla

							if(keyInfo.Key == ConsoleKey.Escape || keyInfo.Key == ConsoleKey.Enter && usuario != "") // Si se presiona la tecla Enter, salir del bucle
							{
								if(keyInfo.Key == ConsoleKey.Escape)
								{
									usuario = "";
									Posicion(48,11);
									Console.WriteLine("                                ");
								}
								
								break;
							}
							else if (keyInfo.Key == ConsoleKey.Backspace && usuario.Length > 0) // Si se presiona la tecla Retroceso, eliminar un carácter del nombre de usuario
							{
								usuario = usuario.Substring(0, usuario.Length - 1);
								Console.Write("\b \b"); // Borra el carácter de la pantalla
							}
							else if (usuario.Length < 21) // Si el límite de caracteres no ha sido alcanzado
							{
								// Permitir letras y espacios, pero no números ni caracteres especiales
								if (char.IsLetterOrDigit(keyInfo.KeyChar) || keyInfo.KeyChar == ' ')
								{
									usuario += keyInfo.KeyChar; // Agregar la tecla al nombre de usuario
									Console.Write(keyInfo.KeyChar); // Mostrar la tecla en la pantalla
								}
							}
						}
						
						Cursor(false);
						
						Color("blanco", "blanco");
						fila = 12; //borrar linea inferior
						for( columna = 42; columna<=78; columna=columna+1 ){
							Posicion(columna, fila);Console.WriteLine(" ");
						}
						
						Color("negro", "blanco");
						Posicion(39,11);
						Console.WriteLine(">  USER: ");
						
						if(!Existe(usuario) && usuario != "")
						{
							Posicion(48,11);
							Console.Write(usuario);
							Color("amarillooscuro", "blanco");
							Console.Write(" !");
						}
						if(Existe(usuario) && usuario != "")
						{
							opcion = 2;
						}
						if(contraseña != "" && usuario != "")
						{
							opcion = 3;
						}
						
					}
				}
			}
			
			if(opcion == 2)
			{
				ColorRandomBlanco();
				Posicion(37,14);
				Console.WriteLine(">");
				Tiempo(20);
				
				ColorRandomBlanco();
				Posicion(37,14);
				Console.WriteLine(" >");
				Tiempo(20);
				
				if(idioma == "español")
				{
					Color("negro", "blanco");
					Posicion(37,14);
					Console.WriteLine("  >  CONTRASEÑA: ");
				}
				if(idioma == "english")
				{
					Color("negro", "blanco");
					Posicion(37,14);
					Console.WriteLine("  >  PASS: ");
				}
				
				fila = 12; //Borrar linea superior
				for( columna = 42; columna<=78; columna=columna+1 ){
					Posicion(columna, fila);Console.WriteLine(" ");
				}
				
				
				if(tecla.Key == ConsoleKey.Enter)
				{
					if(idioma == "español")
					{
						Color("negro", "blanco");
						Posicion(39,14);
						Console.WriteLine("   CONTRASEÑA: ");
						
						Posicion(54,14);
						Console.WriteLine("                              ");
						Posicion(39,11);
						Console.WriteLine(" ");
					}
					if(idioma == "english")
					{
						Color("negro", "blanco");
						Posicion(39,14);
						Console.WriteLine("   PASS: ");
						
						
						Posicion(48,14);
						Console.WriteLine("                              ");
						Posicion(39,11);
						Console.WriteLine(" ");
					}
					
					Posicion(42,15);
					Console.WriteLine("-----------------------------------");
					
					
					if(mostrar == false)
					{
						Color("grisoscuro", "blanco");
						Posicion(76,14);
						Console.Write("  ♦");
					}
					else
					{
						Color("verde", "blanco");
						Posicion(76,14);
						Console.Write("  ♦");
					}
					
					contraseña = "";
					
					Cursor(true);

					
					if(idioma == "español")
					{
						Posicion(54,14);
						while (true)
						{
							
							ConsoleKeyInfo keyInfo = Console.ReadKey(true); // Lee una tecla presionada sin mostrarla en la pantalla
							
							if (keyInfo.Key == ConsoleKey.Escape || keyInfo.Key == ConsoleKey.Enter && contraseña != "") // Si se presiona la tecla Enter, salir del bucle
							{
								Posicion(77,14);
								Console.Write("   ");
								
								if(contraseña != "" && usuario != "" && keyInfo.Key == ConsoleKey.Enter)
								{
									opcion = 3;
								}
								if(keyInfo.Key == ConsoleKey.Escape)
								{
									contraseña = "";
									Posicion(54,14);
									Console.WriteLine("                              ");
								}
								
								break;
							}
							else if (keyInfo.Key == ConsoleKey.Backspace) // Si se presiona la tecla Retroceso, eliminar un carácter de la contraseña
							{
								if (contraseña.Length > 0) // Si se presiona la tecla Retroceso, eliminar un carácter de la contraseña
								{
									contraseña = contraseña.Substring(0, contraseña.Length - 1);
									Console.Write("\b \b"); // Borra el carácter de la pantalla
								}
							}
							else if (keyInfo.KeyChar == '+' || keyInfo.KeyChar == '-'|| keyInfo.Key == ConsoleKey.UpArrow || keyInfo.Key == ConsoleKey.DownArrow ||
							         keyInfo.Key == ConsoleKey.LeftArrow || keyInfo.Key == ConsoleKey.RightArrow) // Ignorar estas teclas
							{
								if(keyInfo.KeyChar == '+')
								{
									contraseña = "";
									mostrar = true;
									
									Color("verde", "blanco");
									Posicion(76,14);
									Console.Write("  ♦");
									
									Posicion(54,14);
									Console.WriteLine("                     ");
									
									Posicion(54,14);
								}
								else if(keyInfo.KeyChar == '-')
								{
									contraseña = "";
									mostrar = false;
									
									Color("grisoscuro", "blanco");
									Posicion(76,14);
									Console.Write("  ♦");
									
									Posicion(54,14);
									Console.WriteLine("                      ");
									
									Posicion(54,14);
								}
								continue;
							}
							else if (contraseña.Length < 21) // Si el límite de caracteres no ha sido alcanzado
							{
								if (!char.IsWhiteSpace(keyInfo.KeyChar))
								{
									contraseña += keyInfo.KeyChar; // Agregar la tecla a la contraseña
									
									if(mostrar == false)
									{
										Azul("blanco");
										Console.Write("*"); // Mostrar un carácter oculto en la pantalla
									}
									else
									{
										Color("negro", "blanco");
										Console.Write(keyInfo.KeyChar);
									}
								}
							}
							else if (keyInfo.Key == ConsoleKey.UpArrow || keyInfo.Key == ConsoleKey.DownArrow || keyInfo.Key == ConsoleKey.LeftArrow || keyInfo.Key == ConsoleKey.RightArrow) // Ignorar las teclas de las flechas
							{
								continue;
							}
						}
						
						Cursor(false);
						
						Posicion(54,16);
						Console.WriteLine("                              ");
						
						Color("blanco", "blanco");
						fila = 15; //borrar linea inferior
						for( columna = 42; columna<=78; columna=columna+1 ){
							Posicion(columna, fila);Console.WriteLine(" ");
						}
						
						Color("negro", "blanco");
						Posicion(39,14);
						Console.WriteLine(">  CONTRASEÑA: ");
					}
					if(idioma == "english")
					{
						Posicion(48,14);
						while (true)
						{
							ConsoleKeyInfo keyInfo = Console.ReadKey(true); // Lee una tecla presionada sin mostrarla en la pantalla
							
							if (keyInfo.Key == ConsoleKey.Escape || keyInfo.Key == ConsoleKey.Enter && contraseña != "") // Si se presiona la tecla Enter, salir del bucle
							{
								Posicion(77,14);
								Console.Write("   ");
								
								if(contraseña != "" && usuario != "" && keyInfo.Key == ConsoleKey.Enter)
								{
									opcion = 3;
								}
								if(keyInfo.Key == ConsoleKey.Escape)
								{
									contraseña = "";
									Posicion(48,14);
									Console.WriteLine("                              ");
								}
								
								break;
							}
							else if (keyInfo.Key == ConsoleKey.Backspace) // Si se presiona la tecla Retroceso, eliminar un carácter de la contraseña
							{
								if (contraseña.Length > 0) // Si se presiona la tecla Retroceso, eliminar un carácter de la contraseña
								{
									contraseña = contraseña.Substring(0, contraseña.Length - 1);
									Console.Write("\b \b"); // Borra el carácter de la pantalla
								}
							}
							else if (keyInfo.KeyChar == '+' || keyInfo.KeyChar == '-'|| keyInfo.Key == ConsoleKey.UpArrow || keyInfo.Key == ConsoleKey.DownArrow ||
							         keyInfo.Key == ConsoleKey.LeftArrow || keyInfo.Key == ConsoleKey.RightArrow) // Ignorar estas teclas
							{
								if(keyInfo.KeyChar == '+')
								{
									contraseña = "";
									mostrar = true;
									
									Color("verde", "blanco");
									Posicion(76,14);
									Console.Write("  ♦");
									
									Posicion(48,14);
									Console.WriteLine("                              ");
									
									Posicion(48,14);
								}
								else if(keyInfo.KeyChar == '-')
								{
									contraseña = "";
									mostrar = false;
									
									Color("grisoscuro", "blanco");
									Posicion(76,14);
									Console.Write("  ♦");
									
									Posicion(48,14);
									Console.WriteLine("                              ");
									
									Posicion(48,14);
								}
								continue;
							}
							else if (contraseña.Length < 21) // Si el límite de caracteres no ha sido alcanzado
							{
								if (!char.IsWhiteSpace(keyInfo.KeyChar))
								{
									contraseña += keyInfo.KeyChar; // Agregar la tecla a la contraseña
									
									if(mostrar == false)
									{
										Azul("blanco");
										Console.Write("*"); // Mostrar un carácter oculto en la pantalla
									}
									else
									{
										Color("negro", "blanco");
										Console.Write(keyInfo.KeyChar);
									}
								}
							}
							else if (keyInfo.Key == ConsoleKey.UpArrow || keyInfo.Key == ConsoleKey.DownArrow || keyInfo.Key == ConsoleKey.LeftArrow || keyInfo.Key == ConsoleKey.RightArrow) // Ignorar las teclas de las flechas
							{
								continue;
							}
						}
						
						Cursor(false);
						
						Posicion(48,16);
						Console.WriteLine("                              ");
						
						Color("blanco", "blanco");
						fila = 15; //borrar linea inferior
						for( columna = 42; columna<=78; columna=columna+1 ){
							Posicion(columna, fila);Console.WriteLine(" ");
						}
						
						Color("negro", "blanco");
						Posicion(39,14);
						Console.WriteLine(">  PASS: ");
					}
				}
			}
			
			if(opcion == 3)
			{
				
				Color("negro", "blanco");
				Posicion(39,11);
				Console.WriteLine(" ");
				Posicion(39,14);
				Console.WriteLine(" ");
				
				Color("blanco", "verdeoscuro");
				for (fila = 20; fila <= 22; fila++) {
					for (columna = 36; columna <= 84; columna++) {
						Posicion(columna, fila);
						Console.WriteLine(" ");
					}
				}
				
				Color("blanco", "blanco");
				fila = 15; //borrar linea inferior
				for( columna = 42; columna<=78; columna=columna+1 ){
					Posicion(columna, fila);Console.WriteLine(" ");
				}
				Color("blanco", "blanco");
				fila = 12; //borrar linea inferior
				for( columna = 42; columna<=78; columna=columna+1 ){
					Posicion(columna, fila);Console.WriteLine(" ");
				}
				
				if(idioma == "español")
				{
					Color("blanco", "verdeoscuro");
					Posicion(53,21);
					Console.WriteLine("INICIAR ");
					
					Posicion(60,21);
					Animacion(" SESION");
				}
				if(idioma == "english")
				{
					Color("blanco", "verdeoscuro");
					Posicion(57,21);
					Console.WriteLine("LOG");
					
					Posicion(61,21);
					Animacion("IN");
				}
				
			}
			if(opcion == 4)
			{
				
				Color("blanco", "rojo");
				for (fila = 23; fila <= 25; fila++) {
					for (columna = 36; columna <= 84; columna++) {
						Posicion(columna, fila);
						Console.WriteLine(" ");
					}
				}
				
				if(idioma == "español")
				{
					Color("blanco", "rojo");
					Posicion(54,24);
					Console.WriteLine("REGIS");
					
					Color("blanco", "rojo");
					Posicion(59,24);
					Animacion("TRARSE");
				}
				if(idioma == "english")
				{
					Color("blanco", "rojo");
					Posicion(57,24);
					Console.WriteLine("SING");
					
					Color("blanco", "rojo");
					Posicion(62,24);
					Animacion("UP");
				}
			}
			if(opcion == 5)
			{
				
				if(idioma == "español")
				{
					Color("CIAN", "negro");
					Posicion(104,28);
					Animacion("ACERCA DE");
					
					Color("negro", "CIAN");
					Posicion(114,28);
					Animacion(" ♦ ");
				}
				if(idioma == "english")
				{
					Color("CIAN", "negro");
					Posicion(104,28);
					Animacion("   ABOUT");
					
					Color("negro", "CIAN");
					Posicion(114,28);
					Animacion(" ♦ ");
				}
			}
			if(opcion == 6)
			{
				if(idioma == "español")
				{
					Color("negro", "CIAN");
					Posicion(2,28);
					Animacion(" ☺ ");
					
					Color("cian", "negro");
					Posicion(6,28);
					Animacion("ESPAÑOL ");
				}
				if(idioma == "english")
				{
					Color("negro", "CIAN");
					Posicion(2,28);
					Animacion(" ☺ ");
					
					Color("cian", "negro");
					Posicion(6,28);
					Animacion("ENGLISH ");
				}
			}
			
			Color("negro", "blanco");
		}
		
		static void Registrarse()
		{
			usuario = "";
			correo = "";
			celular = "";
			contraseña = "";
			opcion = 1;
			
			Reset();
			tui();
			
			Color("blanco", "negro");
			for (fila = 1; fila <= 3; fila++) {
				for (columna = 2; columna <= 117; columna++) {
					Posicion(columna, fila);
					Console.WriteLine(" ");
				}
			}
			
			Color("gris", "azul");
			fila = 4; //borrar linea inferior
			for( columna = 3; columna<=116; columna=columna+1 ){
				Posicion(columna, fila);Console.WriteLine(" ");
			}
			Color("negro", "azul");
			fila = 28; //borrar linea inferior
			for( columna = 3; columna<=116; columna=columna+1 ){
				Posicion(columna, fila);Console.WriteLine("▄");
			}
			Color("azul", "blanco");
			fila = 27; //borrar linea inferior
			for( columna = 3; columna<=116; columna=columna+1 ){
				Posicion(columna, fila);Console.WriteLine("▄");
			}
			
			Color("blanco", "azul");
			for (fila = 22; fila <= 27; fila++) {
				for (columna = 60; columna <= 61; columna++) {
					Posicion(columna, fila);
					Console.WriteLine(" ");
				}
			}
			for (fila = 24; fila <= 27; fila++) {
				for (columna = 66; columna <= 67; columna++) {
					Posicion(columna, fila);
					Console.WriteLine(" ");
				}
			}
			
			Color("blanco", "negro");
			for (fila = 4; fila <= 6; fila++) {
				for (columna = 87; columna <= 88; columna++) {
					Posicion(columna, fila);
					Console.WriteLine(" ");
				}
			}
			for (fila = 4; fila <= 9; fila++) {
				for (columna = 94; columna <= 95; columna++) {
					Posicion(columna, fila);
					Console.WriteLine(" ");
				}
			}
			
			do{
				Cursor(false);
				
				if(usuarioValido && correoValido && celularValido && contraseñaValida)
				{
					cuentaCreada = true;
				}
				else
				{
					cuentaCreada = false;
				}
				
				
				Posicion(20,2);
				Color("blanco", "negro");
				Console.WriteLine("                                                                                ");
				
				if(idioma == "español")
				{
					Posicion(51,2);
					Color("cian", "negro");
					Console.WriteLine("REGISTRARSE");
					
					Posicion(5,2);
					Color("cian", "negro");
					Console.WriteLine("←");
					
					Posicion(18,9);
					Color("NEGRO", "BLANCO");
					Console.WriteLine("     USUARIO                                       ");
					
					Posicion(18,13);
					Console.WriteLine("     CORREO                                        ");
					
					Posicion(18,17);
					Console.WriteLine("     CELULAR                                       ");
					
					Posicion(18,21);
					Console.WriteLine("     CONTRASEÑA                                    ");
					
					Posicion(18,9);
					Console.WriteLine("     USUARIO");
					
					if(usuario != "")
					{
						Color("negro", "blanco");
						Posicion(18,9);
						Console.WriteLine("     USUARIO: " + usuario);
					}
					if(correo != "")
					{
						Color("negro", "blanco");
						Posicion(18,13);
						Console.Write("     CORREO: " + correo);
						Console.Write("@pascual...");
					}
					if(celular != "")
					{
						Color("negro", "blanco");
						Posicion(18,17);
						Console.WriteLine("     CELULAR: " + celular);
					}
					if(contraseña != "")
					{
						if(mostrar == true)
						{
							Color("negro", "blanco");
							Posicion(18,21);
							Console.WriteLine("     CONTRASEÑA: " + contraseña);
						}
						else
						{
							Color("negro", "blanco");
							Posicion(18,21);
							Console.WriteLine("     CONTRASEÑA: " + "oculta");
						}
					}
				}
				if(idioma == "english")
				{
					Posicion(55,2);
					Color("cian", "negro");
					Console.WriteLine("SING IN");
					
					Posicion(5,2);
					Color("cian", "negro");
					Console.WriteLine("←");
					
					Posicion(18,9);
					Color("NEGRO", "BLANCO");
					Console.WriteLine("     USER                                          ");
					
					Posicion(18,13);
					Console.WriteLine("     MAIL                                          ");
					
					Posicion(18,17);
					Console.WriteLine("     CELL PHONE                                    ");
					
					Posicion(18,21);
					Console.WriteLine("     PASSWORD                                      ");
					
					Posicion(18,9);
					Console.WriteLine("     USER");
					
					if(usuario != "")
					{
						Color("negro", "blanco");
						Posicion(18,9);
						Console.WriteLine("     USER: " + usuario);
					}
					if(correo != "")
					{
						Color("negro", "blanco");
						Posicion(18,13);
						Console.Write("     MAIL: " + correo);
						Console.Write("@pascual...");
					}
					if(celular != "")
					{
						Color("negro", "blanco");
						Posicion(18,17);
						Console.WriteLine("     CELL PHONE: " + celular);
					}
					if(contraseña != "")
					{
						if(mostrar == true)
						{
							Color("negro", "blanco");
							Posicion(18,21);
							Console.WriteLine("     PASSWORD: " + contraseña);
						}
						else
						{
							Color("negro", "blanco");
							Posicion(18,21);
							Console.WriteLine("     PASSWORD: " + "HIDDEN");
						}
					}
				}
				
				if(opcion == 1)
				{
					if(usuario != "")
					{
						Posicion(21,9);
						Color("blanco", "blanco");
						Console.WriteLine("                                        ");
						
						Posicion(22,9);
						Color("NEGRO", "gris");
						Console.WriteLine("█" + usuario + "█");
						
						Tiempo(30);
						
						Posicion(20,9);
						Color("NEGRO", "gris");
						Console.Write("█  " + usuario + "  █");
						
						Color("grisoscuro", "blanco");
						Console.Write("  ☻");
					}
					else
					{
						if(idioma == "español")
						{
							Posicion(21,9);
							Color("blanco", "blanco");
							Console.WriteLine("                                        ");
							
							Posicion(21,9);
							Color("NEGRO", "gris");
							Console.WriteLine("█ USUARIO █");
							
							Tiempo(30);
							
							Posicion(20,9);
							Color("NEGRO", "gris");
							Console.Write("█  USUARIO  █");
							
							Color("grisoscuro", "blanco");
							Console.Write("  ☻");
						}
						if(idioma == "english")
						{
							Posicion(21,9);
							Color("blanco", "blanco");
							Console.WriteLine("                                        ");
							
							Posicion(21,9);
							Color("NEGRO", "gris");
							Console.WriteLine("█ USER █");
							
							Tiempo(30);
							
							Posicion(20,9);
							Color("NEGRO", "gris");
							Console.Write("█  USER  █");
							
							Color("grisoscuro", "blanco");
							Console.Write("  ☻");
						}
					}
				}
				if(opcion == 2)
				{
					if(correo != "")
					{
						Posicion(21,13);
						Color("blanco", "blanco");
						Console.WriteLine("                                        ");
						
						Posicion(22,13);
						Color("NEGRO", "gris");
						Console.WriteLine("█" + correo + "█");
						
						Tiempo(30);
						
						Posicion(20,13);
						Color("NEGRO", "gris");
						Console.Write("█  " + correo + "  █");
						
						Color("grisoscuro", "blanco");
						Console.Write("  @pascualbravo.edu.co");
					}
					else
					{
						if(idioma == "español")
						{
							Posicion(21,13);
							Color("blanco", "blanco");
							Console.WriteLine("                                        ");
							
							Posicion(21,13);
							Color("NEGRO", "gris");
							Console.WriteLine("█ CORREO █");
							
							Tiempo(30);
							
							Posicion(20,13);
							Color("NEGRO", "gris");
							Console.Write("█  CORREO  █");
							
							Color("grisoscuro", "blanco");
							Console.Write("  @pascualbravo.edu.co");
						}
						if(idioma == "english")
						{
							Posicion(21,13);
							Color("blanco", "blanco");
							Console.WriteLine("                                        ");
							
							Posicion(21,13);
							Color("NEGRO", "gris");
							Console.WriteLine("█ MAIL █");
							
							Tiempo(30);
							
							Posicion(20,13);
							Color("NEGRO", "gris");
							Console.Write("█  MAIL  █");
							
							Color("grisoscuro", "blanco");
							Console.Write("  @pascualbravo.edu.co");
						}
					}
				}
				if(opcion == 3)
				{
					if(celular != "")
					{
						Posicion(21,17);
						Color("blanco", "blanco");
						Console.WriteLine("                                        ");
						
						Posicion(22,17);
						Color("NEGRO", "gris");
						Console.Write("█" + celular + "█");
						
						Tiempo(30);
						
						Posicion(20,17);
						Color("NEGRO", "gris");
						Console.Write("█  " + celular + "  █");
						
						Color("grisoscuro", "blanco");
						Console.Write("  #");
					}
					else
					{
						if(idioma == "español")
						{
							Posicion(21,17);
							Color("blanco", "blanco");
							Console.WriteLine("                                        ");
							
							Posicion(21,17);
							Color("NEGRO", "gris");
							Console.WriteLine("█ CELULAR █");
							
							Tiempo(30);
							
							Posicion(20,17);
							Color("NEGRO", "gris");
							Console.Write("█  CELULAR  █");
							
							Color("grisoscuro", "blanco");
							Console.Write("  #");
						}
						if(idioma == "english")
						{
							Posicion(21,17);
							Color("blanco", "blanco");
							Console.WriteLine("                                        ");
							
							Posicion(21,17);
							Color("NEGRO", "gris");
							Console.WriteLine("█ CELL PHONE █");
							
							Tiempo(30);
							
							Posicion(20,17);
							Color("NEGRO", "gris");
							Console.Write("█  CELL PHONE  █");
							
							Color("grisoscuro", "blanco");
							Console.Write("  #");
						}
					}
				}
				if(opcion == 4)
				{
					if(contraseña != "")
					{
						Posicion(21,21);
						Color("blanco", "blanco");
						Console.WriteLine("                                        ");
						
						if(mostrar == true)
						{
							Posicion(22,21);
							Color("NEGRO", "gris");
							Console.WriteLine("█" + contraseña + "█");
							
							Tiempo(30);
							
							Posicion(20,21);
							Color("NEGRO", "gris");
							Console.Write("█  " + contraseña + "  █");
						}
						else
						{
							if(idioma == "español")
							{
								Posicion(22,21);
								Color("NEGRO", "gris");
								Console.WriteLine("█ " + "OCULTA" + " █");
								
								Tiempo(10);
								
								Posicion(20,21);
								Color("NEGRO", "gris");
								Console.Write("█   " + "OCULTA" + "   █");
							}
							if(idioma == "english")
							{
								Posicion(22,21);
								Color("NEGRO", "gris");
								Console.WriteLine("█ " + "HIDDEN" + " █");
								
								Tiempo(10);
								
								Posicion(20,21);
								Color("NEGRO", "gris");
								Console.Write("█   " + "HIDDEN" + "   █");
							}
						}
					}
					else
					{
						if(idioma == "español")
						{
							Posicion(21,21);
							Color("blanco", "blanco");
							Console.WriteLine("                                        ");
							
							Posicion(21,21);
							Color("NEGRO", "gris");
							Console.WriteLine("█ CONTRASEÑA █");
							
							Tiempo(30);
							
							Posicion(20,21);
							Color("NEGRO", "gris");
							Console.Write("█  CONTRASEÑA  █");
						}
						if(idioma == "english")
						{
							Posicion(21,21);
							Color("blanco", "blanco");
							Console.WriteLine("                                        ");
							
							Posicion(21,21);
							Color("NEGRO", "gris");
							Console.WriteLine("█ PASSWORD █");
							
							Tiempo(30);
							
							Posicion(20,21);
							Color("NEGRO", "gris");
							Console.Write("█  PASSWORD  █");
						}
					}
					
					if(mostrar == false)
					{
						Color("grisoscuro", "blanco");
						Console.Write("  ♦");
					}
					else
					{
						Color("verde", "blanco");
						Console.Write("  ♦");
					}
					
				}
				
				if(opcion != 10)
				{
					anterior = opcion;
				}

				CrearCuenta();
				
				Precionar();
				
				if(tecla.Key == ConsoleKey.Enter)
				{
					switch(opcion)
					{
							
						case 1:
							
							Cursor(true);
							usuario = "";
							
							Posicion(20,9);
							Color("negro", "gris");
							Console.WriteLine("  █                     █");
							
							Tiempo(5);
							
							Posicion(20,9);
							Color("negro", "gris");
							Console.WriteLine("█                         █");
							
							if(idioma == "español")
							{
								Posicion(40,2);
								Color("blanco", "negro");
								Console.Write("Tu nombre unico para iniciar sesion.");
							}
							if(idioma == "english")
							{
								Posicion(43,2);
								Color("blanco", "negro");
								Console.Write("Your unique name to log in.");
							}
							
							Color("negro", "gris");
							Posicion(23,9);
							while (true)
							{
								ConsoleKeyInfo keyInfo = Console.ReadKey(true); // Lee una tecla presionada sin mostrarla en la pantalla

								if (keyInfo.Key == ConsoleKey.Escape || keyInfo.Key == ConsoleKey.Enter && usuario != "") // Si se presiona la tecla Enter, salir del bucle
								{
									if(Existe(usuario) && usuario != "")
									{
										
										usuario = "";
										
										Posicion(20,9);
										Color("negro", "gris");
										Console.WriteLine("█                         █");
										
										Color("rojo", "negro");
										Posicion(34,2);
										Console.Write("¡ ");
										
										if(idioma == "español")
										{
											string[] dialogo = {
												"Ese usuario ya existe, por favor elije otro",
												"Usuario ya existente, elije otro nombre",
												"No disponible, elije otro nombre por favor",
												"Usuario ya ocupado, por favor elije otro",
											};

											Random rnd = new Random();
											int indice = rnd.Next(0, dialogo.Length);

											Color("blanco", "negro");
											Console.Write(dialogo[indice]);
										}
										if(idioma == "english")
										{
											string[] dialogo = {
												"That user already exists, please choose another",
												"Existing user, choose another name",
												"Not available, choose another name please",
												"User already busy, please choose another",
											};

											Random rnd = new Random();
											int indice = rnd.Next(0, dialogo.Length);

											Color("blanco", "negro");
											Console.Write(dialogo[indice]);
										}
										
										Color("rojo", "negro");
										Console.Write(" !                ");
										
										Color("negro", "gris");
										Posicion(23,9);
									}
									else
									{
										if(keyInfo.Key == ConsoleKey.Escape)
										{
											usuario = "";
										}
										else
										{
											opcion ++;
										}
										break;
									}
								}
								else if (keyInfo.Key == ConsoleKey.Backspace && usuario.Length > 0) // Si se presiona la tecla Retroceso, eliminar un carácter del nombre de usuario
								{
									usuario = usuario.Substring(0, usuario.Length - 1);
									Console.Write("\b \b"); // Borra el carácter de la pantalla
								}
								else if (usuario.Length < 21) // Si el límite de caracteres no ha sido alcanzado
								{
									// Permitir letras y espacios, pero no números ni caracteres especiales
									if (char.IsLetterOrDigit(keyInfo.KeyChar) || keyInfo.KeyChar == ' ')
									{
										usuario += keyInfo.KeyChar; // Agregar la tecla al nombre de usuario
										Console.Write(keyInfo.KeyChar); // Mostrar la tecla en la pantalla
									}
								}
								
							}

							
							if(usuario != "")
							{
								usuarioValido = true;
							}
							else
							{
								usuarioValido = false;
							}
							
							break;
							
						case 2:
							
							Cursor(true);
							correo = "";
							
							Posicion(20,13);
							Color("negro", "gris");
							Console.WriteLine("  █                     █");
							
							Tiempo(5);
							
							Posicion(20,13);
							Color("negro", "gris");
							Console.Write("█                         █");
							
							Color("negro", "blanco");
							Console.WriteLine("  @pascualbra...");
							
							if(idioma == "español")
							{
								Posicion(35,2);
								Color("blanco", "negro");
								Console.Write("La direcion de correo se pondra automaticamente.");
							}
							if(idioma == "english")
							{
								Posicion(37,2);
								Color("blanco", "negro");
								Console.Write("The email address will be set automatically.");
							}
							
							Color("negro", "gris");
							Posicion(23,13);
							while (true)
							{
								ConsoleKeyInfo keyInfo = Console.ReadKey(true); // Lee una tecla presionada sin mostrarla en la pantalla

								if (keyInfo.Key == ConsoleKey.Escape || keyInfo.Key == ConsoleKey.Enter && correo != "") // Si se presiona la tecla Enter, salir del bucle
								{
									if(keyInfo.Key == ConsoleKey.Escape)
									{
										correo = "";
									}
									break;
								}
								else if (keyInfo.Key == ConsoleKey.Backspace && correo.Length > 0) // Si se presiona la tecla Retroceso, eliminar un carácter del nombre de usuario
								{
									correo = correo.Substring(0, correo.Length - 1);
									Console.Write("\b \b"); // Borra el carácter de la pantalla
								}
								else if (correo.Length < 21) // Si el límite de caracteres no ha sido alcanzado
								{
									// Permitir letras, números y el carácter especial "."
									if (char.IsLetterOrDigit(keyInfo.KeyChar) || keyInfo.KeyChar == '.' && !correo.Contains("."))
									{
										correo += keyInfo.KeyChar; // Agregar la tecla al nombre de usuario
										Console.Write(keyInfo.KeyChar); // Mostrar la tecla en la pantalla
									}
								}
							}
							
							if(correo != "")
							{
								correoValido = true;
								correoPascual = correo + "@pascualbravo.edu.co";
								opcion++;
							}
							else
							{
								correoValido = false;
							}
							
							break;
							
						case 3:
							
							Cursor(true);
							celular = "";
							
							Posicion(20,17);
							Color("negro", "gris");
							Console.WriteLine("  █                     █");
							
							Tiempo(5);
							
							Posicion(20,17);
							Color("negro", "gris");
							Console.Write("█                         █");
							
							Color("grisoscuro", "blanco");
							Console.Write("  #");
							
							if(idioma == "español")
							{
								Posicion(47,2);
								Color("blanco", "negro");
								Console.Write("Tu numero de celular");
							}
							if(idioma == "english")
							{
								Posicion(47,2);
								Color("blanco", "negro");
								Console.Write("Your cell phone number");
							}
							
							Color("negro", "gris");
							Posicion(23,17);
							while (true)
							{
								ConsoleKeyInfo keyInfo = Console.ReadKey(true);

								if (keyInfo.Key == ConsoleKey.Escape || keyInfo.Key == ConsoleKey.Enter && celular != "")
								{
									if(celular == "")
									{
										break;
									}
									if (celular.Length != 10)
									{
										celular = "";
										
										Posicion(20,17);
										Color("negro", "gris");
										Console.Write("█                         █");
										
										Posicion(27,2);
										Color("rojo", "negro");
										Console.Write("¡ ");
										
										if(idioma == "español")
										{
											Color("blanco", "negro");
											Console.Write("El número de celular debe tener exactamente 10 caracteres.");
										}
										if(idioma == "english")
										{
											Color("blanco", "negro");
											Console.Write("The cell phone number must be exactly 10 characters.");
										}
										
										Color("rojo", "negro");
										Console.Write(" !");
										
										Color("negro", "gris");
										Posicion(23,17);
									}
									else
									{
										if(keyInfo.Key == ConsoleKey.Escape)
										{
											celular = "";
										}
										else
										{
											opcion ++;
										}
										break;
									}
								}
								else if (keyInfo.Key == ConsoleKey.Backspace && celular.Length > 0)
								{
									celular = celular.Substring(0, celular.Length - 1);
									Console.Write("\b \b");
								}
								else if (char.IsDigit(keyInfo.KeyChar) && celular.Length < 10)
								{
									celular += keyInfo.KeyChar;
									Console.Write(keyInfo.KeyChar);
								}
								
							}
							
							if(celular != "")
							{
								celularValido = true;
							}
							else
							{
								celularValido = false;
							}
							
							break;
							
						case 4:
							
							Cursor(true);
							contraseña = "";
							
							if(idioma == "español")
							{
								Posicion(35,2);
								Color("blanco", "negro");
								Console.WriteLine("Usa \"+\" y \"-\" para mostrar u ocultar la contraseña.");
							}
							if(idioma == "english")
							{
								Posicion(35,2);
								Color("blanco", "negro");
								Console.WriteLine("Use \"+\" and \"-\" to show or hide the password");
							}
							
							Posicion(20,21);
							Color("negro", "gris");
							Console.WriteLine("  █                     █");
							
							Tiempo(5);
							
							Posicion(20,21);
							Color("negro", "gris");
							Console.Write("█                         █");
							
							if(mostrar == false)
							{
								Color("grisoscuro", "blanco");
								Console.Write("  ♦");
							}
							else
							{
								Color("verde", "blanco");
								Console.Write("  ♦");
							}
							
							Color("negro", "gris");
							Posicion(23,21);
							while (true)
							{
								ConsoleKeyInfo keyInfo = Console.ReadKey(true); // Lee una tecla presionada sin mostrarla en la pantalla

								if (keyInfo.Key == ConsoleKey.Escape || keyInfo.Key == ConsoleKey.Enter && contraseña != "") // Si se presiona la tecla Enter, salir del bucle
								{
									if (contraseña == "")
									{
										break;
									}
									else if (contraseña.Length < 4)
									{
										// Restablecer la contraseña y mostrar un mensaje de error
										contraseña = "";

										Posicion(20, 2);
										Color("blanco", "negro");
										Console.WriteLine("                                                                                ");

										Posicion(35, 2);
										Color("rojo", "negro");
										Console.Write("¡ ");

										if(idioma == "español")
										{
											Color("blanco", "negro");
											Console.Write("La contraseña debe tener al menos 4 caracteres");
										}
										if(idioma == "english")
										{
											Color("blanco", "negro");
											Console.Write("Password must be at least 4 characters");
										}

										Color("rojo", "negro");
										Console.Write(" !");

										Posicion(20, 21);
										Color("negro", "gris");
										Console.WriteLine("█                         █");

										Color("negro", "gris");
										Posicion(23, 21);
									}
									else if (!contraseña.Any(char.IsDigit) && !contraseña.Any(IsSpecialCharacter))
									{
										// Restablecer la contraseña y mostrar un mensaje de error
										contraseña = "";

										Posicion(20, 2);
										Color("blanco", "negro");
										Console.WriteLine("                                                                                ");

										Posicion(26, 2);
										Color("rojo", "negro");
										Console.Write("¡ ");

										if(idioma == "español")
										{
											Color("blanco", "negro");
											Console.Write("La contraseña debe contener al menos un número o un carácter especial");
										}
										if(idioma == "english")
										{
											Color("blanco", "negro");
											Console.Write("The password must contain at least one number or one special character");
										}

										Color("rojo", "negro");
										Console.Write(" !");

										Posicion(20, 21);
										Color("negro", "gris");
										Console.WriteLine("█                         █");

										Color("negro", "gris");
										Posicion(23, 21);
									}
									else
									{
										if(keyInfo.Key == ConsoleKey.Escape)
										{
											contraseña = "";
										}
										break;
									}
								}
								else if (keyInfo.Key == ConsoleKey.Backspace) // Si se presiona la tecla Retroceso
								{
									if (contraseña.Length > 0) // Verificar si hay caracteres para borrar
									{
										contraseña = contraseña.Substring(0, contraseña.Length - 1);
										Console.Write("\b \b"); // Borra el carácter de la pantalla
									}
								}
								else if (keyInfo.KeyChar == '+' || keyInfo.KeyChar == '-') // Ignorar las teclas '+' y '-'
								{
									if (keyInfo.KeyChar == '+')
									{
										// Restablecer la contraseña y configurar para mostrarla
										Posicion(20, 21);
										Color("negro", "gris");
										Console.Write("█                         █");

										Color("verde", "blanco");
										Console.Write("  ♦");

										contraseña = "";
										mostrar = true;

										Color("negro", "gris");
										Posicion(23, 21);
									}
									else if (keyInfo.KeyChar == '-')
									{
										// Restablecer la contraseña y configurar para ocultarla
										Posicion(20, 21);
										Color("negro", "gris");
										Console.Write("█                         █");

										Color("grisoscuro", "blanco");
										Console.Write("  ♦");

										contraseña = "";
										mostrar = false;

										Color("negro", "gris");
										Posicion(23, 21);
									}
									continue;
								}
								else if (keyInfo.Key == ConsoleKey.UpArrow || keyInfo.Key == ConsoleKey.DownArrow || keyInfo.Key == ConsoleKey.LeftArrow || keyInfo.Key == ConsoleKey.RightArrow) // Ignorar las teclas de las flechas
								{
									continue;
								}
								else if (contraseña.Length < 21) // Si el límite de caracteres no ha sido alcanzado
								{
									if (!char.IsWhiteSpace(keyInfo.KeyChar))
									{
										contraseña += keyInfo.KeyChar; // Agregar la tecla a la contraseña
										if (mostrar == false)
										{
											Console.Write("*"); // Mostrar un carácter oculto en la pantalla
										}
										else
										{
											Console.Write(keyInfo.KeyChar);
										}
									}
								}
							}

							if (contraseña != "")
							{
								contraseñaValida = true;
							}
							else
							{
								contraseñaValida = false;
							}
							
							break;
							
						case 10:
							
							if(cuentaCreada)
							{

								correo = correoPascual;
								puntos = "0";
								
								// Guardar datos del primer usuario
								Guardar("Datos.txt", correo, celular, contraseña, usuario, puntos);
								
								usuario = "";
								correo = "";
								celular = "";
								contraseña = "";
								
								usuarioValido = false;
								correoValido = false;
								celularValido = false;
								contraseñaValida = false;
								
								salir = true;
							}
							else
							{
								if(idioma == "español")
								{
									if(!usuarioValido)
									{
										Color("rojo", "blanco");
										Posicion(32,9);
										Console.Write("!");
									}
									if(!correoValido)
									{
										Color("rojo", "blanco");
										Posicion(31,13);
										Console.Write("!");
									}
									if(!celularValido)
									{
										Color("rojo", "blanco");
										Posicion(32,17);
										Console.Write("!");
									}
									if(!contraseñaValida)
									{
										Color("rojo", "blanco");
										Posicion(35,21);
										Console.Write("!");
									}
								}
								if(idioma == "english")
								{
									if(!usuarioValido)
									{
										Color("rojo", "blanco");
										Posicion(29,9);
										Console.Write("!");
									}
									if(!correoValido)
									{
										Color("rojo", "blanco");
										Posicion(29,13);
										Console.Write("!");
									}
									if(!celularValido)
									{
										Color("rojo", "blanco");
										Posicion(35,17);
										Console.Write("!");
									}
									if(!contraseñaValida)
									{
										Color("rojo", "blanco");
										Posicion(33,21);
										Console.Write("!");
									}
								}
								
								Color("blanco", "rojooscuro");
								Posicion(70,18);
								Console.WriteLine("                ");
								Posicion(70,19);
								Console.WriteLine("                ");
								Posicion(70,20);
								Console.WriteLine("                ");
								Posicion(70,19);
								
								if(idioma == "español")
								{
									Animacion("  FALTAN DATOS ");
								}
								if(idioma == "english")
								{
									Animacion("  MISSING DATA ");
								}
								
								Precionar();
							}
							
							break;
					}
				}
				
				switch(tecla.Key)
				{
					case ConsoleKey.Escape:
						
						usuario = "";
						correo = "";
						celular = "";
						contraseña = "";
						
						usuarioValido = false;
						correoValido = false;
						celularValido = false;
						contraseñaValida = false;
						
						salir = true;
						break;
						
					case ConsoleKey.UpArrow:
						
						if (opcion > 0 && opcion < 5) // Evita que opción baje si ya es igual a 1
						{
							opcion = Math.Max(opcion - 1, 1);
						}
						break;
						
					case ConsoleKey.DownArrow:
						
						if (opcion > 0 && opcion < 5) // Evita que opción suba si ya es igual a 5
						{
							opcion = Math.Min(opcion + 1, 4);
						}
						break;
						
					case ConsoleKey.RightArrow:
						
						opcion = 10;
						break;
						
					case ConsoleKey.LeftArrow:
						
						opcion = anterior;
						break;
				}
				
			}while(salir == false);
			
			Color("negro", "negro");
			Reset();
			
			opcion = 1;
			mostrar = false;
			salir = false;
			
			Color("negro", "blanco");
			for (fila = 3; fila <=19; fila++) {
				for (columna = 36; columna <= 84; columna++) {
					Posicion(columna, fila);
					Console.WriteLine(" ");
				}
			}
			
		}
		static void GPT()
		{
			Color("negro", "negro");
			for (fila = 17; fila <=19; fila++) {
				for (columna = 3; columna <= 34; columna++) {
					Posicion(columna, fila);
					Console.WriteLine(" ");
				}
			}
			
			Color("grisoscuro", "negro");
			fila = 26; //linea de en medio
			for( columna = 37; columna<=115; columna=columna+1 ){
				Posicion(columna, fila);Console.WriteLine(" ");
			}
			
			fila = 25; //linea superior
			for( columna = 37; columna<=114; columna=columna+1 ){
				Posicion(columna, fila);Console.WriteLine("▀");
			}
			
			fila = 27; //linea inferior
			for( columna = 37; columna<=114; columna=columna+1 ){
				Posicion(columna, fila);Console.WriteLine("▄");
			}
			
			columna = 36; //lado iskierdo
			for( fila = 25; fila<=27; fila=fila+1 ){
				Posicion(columna, fila);Console.WriteLine("█");
			}
			
			columna = 115; //lado derecho
			for( fila = 25; fila<=27; fila=fila+1 ){
				Posicion(columna, fila);Console.WriteLine("█");
			}
			
			Color("grisoscuro", "negro");
			fila = 2; //linea superior
			for( columna = 37; columna<=114; columna=columna+1 ){
				Posicion(columna, fila);Console.WriteLine("▀");
			}
			
			fila = 23; //linea inferior
			for( columna = 37; columna<=114; columna=columna+1 ){
				Posicion(columna, fila);Console.WriteLine("▄");
			}
			
			columna = 36; //lado iskierdo
			for( fila = 2; fila<=23; fila=fila+1 ){
				Posicion(columna, fila);Console.WriteLine("█");
			}
			
			columna = 115; //lado derecho
			for( fila = 2; fila<=23; fila=fila+1 ){
				Posicion(columna, fila);Console.WriteLine("█");
			}
			
			Color("negro", "negro");
			for (fila = 3; fila <=22; fila++) {
				for (columna = 37; columna <= 114; columna++) {
					Posicion(columna, fila);
					Console.WriteLine(" ");
				}
			}
			
			
			// Verificar si se presionó una tecla de flecha
			if (tecla.Key != ConsoleKey.UpArrow && tecla.Key != ConsoleKey.DownArrow &&
			    tecla.Key != ConsoleKey.LeftArrow || tecla.Key == ConsoleKey.Enter)
			{
				
				Random rnd = new Random();
				int numeroAleatorio = rnd.Next(1, 4); // Genera un número aleatorio
				
				if(numeroAleatorio == 1)
				{
					string[] arteASCII1 =
					{
						"░░▄▀▀▀▄░▄▄░░░░░░╠▓░░░░",
						"░░░▄▀▀▄█▄░▀▄░░░▓╬▓▓▓░░",
						"░░▀░░░░█░▀▄░░░▓▓╬▓▓▓▓░",
						"░░░░░░▐▌░░░░▀▀███████▀",
						"▒▒▄██████▄▒▒▒▒▒▒▒▒▒▒▒▒"
					};

					// Imprimir el arte ASCII con Posicion()
					for (int i = 0; i < arteASCII1.Length; i++)
					{
						Color("negro", "blanco");
						Posicion(62, 8 + i);
						Console.WriteLine(arteASCII1[i]);
					}
				}
				if(numeroAleatorio == 2)
				{
					string[] arteASCII2 =
					{
						"────────────────▄▄───▐█",
						"───▄▄▄───▄██▄──█▀───█─▄",
						"─▄██▀█▌─██▄▄──▐█▀▄─▐█▀",
						"▐█▀▀▌───▄▀▌─▌─█─▌──▌─▌",
						"▌▀▄─▐──▀▄─▐▄─▐▄▐▄─▐▄─▐▄"
					};

					// Imprimir el arte ASCII con Posicion()
					for (int i = 0; i < arteASCII2.Length; i++)
					{
						Color("negro", "blanco");
						Posicion(62, 8 + i);
						Console.WriteLine(arteASCII2[i]);
					}
				}
				if(numeroAleatorio == 3)
				{
					string[] arteASCII3 =
					{
						"▐▓█▀▀▀▀▀▀▀▀▀█▓▌░▄▄▄▄▄░",
						"▐▓█░░▀░░▀▄░░█▓▌░█▄▄▄█░",
						"▐▓█░░▄░░▄▀░░█▓▌░█▄▄▄█░",
						"▐▓█▄▄▄▄▄▄▄▄▄█▓▌░█████░",
						"░░░░▄▄███▄▄░░░░░█████░"
					};

					// Imprimir el arte ASCII con Posicion()
					for (int i = 0; i < arteASCII3.Length; i++)
					{
						Color("negro", "blanco");
						Posicion(62, 8 + i);
						Console.WriteLine(arteASCII3[i]);
					}
				}
				if(numeroAleatorio == 4)
				{
					string[] arteASCII4 =
					{
						"──────────────▄▀█▀█▀▄",
						"─────────────▀▀▀▀▀▀▀▀▀",
						"─────────────▄─░░░░░▄",
						"───█──▄─▄───▐▌▌░░░░░▌▌",
						"▌▄█▐▌▐█▐▐▌█▌█▌█░░░░░▌▌"
					};

					// Imprimir el arte ASCII con Posicion()
					for (int i = 0; i < arteASCII4.Length; i++)
					{
						Color("negro", "blanco");
						Posicion(62, 8 + i);
						Console.WriteLine(arteASCII4[i]);
					}
				}
				
				if(idioma == "español")
				{
					Color("blanco", "negro");
					Posicion(12,18);
					Console.WriteLine("   EN CHAT   ");
					
					Color("gris", "negro");
					Posicion(46,22);
					Console.Write("■ Preciona la tecla");
					Color("cian", "negro");
					Console.Write(" → ");
					Color("gris", "negro");
					Console.Write("para volver a la pregunta anterior");
					
					Color("blanco", "negro");
					Posicion(68,14);
					Console.Write("Chat con IA");
				}
				if(idioma == "english")
				{
					Color("blanco", "negro");
					Posicion(12,18);
					Console.WriteLine("   IN CHAT   ");
					
					Color("gris", "negro");
					Posicion(46,22);
					Console.Write("■ Press the key");
					Color("cian", "negro");
					Console.Write(" → ");
					Color("gris", "negro");
					Console.Write("to go back to the previous question");
					Color("blanco", "negro");
					Posicion(68,14);
					Console.Write("Chat with AI");
					
				}
				
				Color("negro", "cian");
				Posicion(111,26);
				Console.Write(" → ");
				
				Color("blanco", "negro");
				Posicion(40,26);
				while (true)
				{
					Cursor(true);
					ConsoleKeyInfo keyInfo = Console.ReadKey(true);
					
					if (keyInfo.Key == ConsoleKey.UpArrow ||tecla.Key == ConsoleKey.DownArrow ||
					    keyInfo.Key == ConsoleKey.LeftArrow || keyInfo.Key == ConsoleKey.Escape)
					{
						break;
					}
					else if (keyInfo.Key == ConsoleKey.Backspace && pregunta.Length > 0)
					{
						pregunta = pregunta.Substring(0, pregunta.Length - 1);
						Console.Write("\b \b");
					}
					else if (pregunta.Length < 68 && !char.IsControl(keyInfo.KeyChar))
					{
						pregunta += keyInfo.KeyChar;
						Console.Write(keyInfo.KeyChar);
					}
					if(keyInfo.Key == ConsoleKey.RightArrow)
					{
						Cursor(false);
						Color("negro", "azul");
						Posicion(111,26);
						Animacion(" → ");
						
						Color("negro", "cian");
						Posicion(111,26);
						Animacion(" → ");
						
						pregunta = preguntaAnterior;
						Color("blanco", "negro");
						Posicion(40,26);
						Console.Write(pregunta);
					}
					if(keyInfo.Key == ConsoleKey.Enter)
					{
						Cursor(false);

						
						if(pregunta != "")
						{
							Color("negro", "negro");
							for (fila = 3; fila <=22; fila++) {
								for (columna = 37; columna <= 114; columna++) {
									Posicion(columna, fila);
									Console.WriteLine(" ");
								}
							}
							
							Color("negro", "verde");
							Posicion(111,26);
							Animacion(" → ");
							
							Color("negro", "cian");
							Posicion(111,26);
							Animacion(" = ");
							
							chats++;
						}
						
						Color("negro", "cian");
						Posicion(111,26);
						Animacion(" = ");
						
						Color("blanco", "negro");
						Posicion(38,26);
						Console.Write("                                                                        ");
						
						Color("cian", "negro");
						Posicion(40,5);
						Console.Write(pregunta);
						
						Tiempo(30);
						Color("blanco", "negro");
						
						if(idioma == "english")
						{
							RespuestasEnglish();
						}
						else if(idioma == "español")
						{
							RespuestasEspañol();
						}

						
						Color("negro", "cian");
						Posicion(111,26);
						Animacion(" → ");
						
						preguntaAnterior = pregunta;
						pregunta = "";
						
						Color("blanco", "negro");
						Posicion(40,26);
						
					}
				}
				
				Cursor(false);
				
				Color("negro", "blanco");
				for (fila = 2; fila <=27; fila++) {
					for (columna = 35; columna <= 116; columna++) {
						Posicion(columna, fila);
						Console.WriteLine(" ");
					}
				}
			}
		}
		
		static void RespuestasEspañol()
		{
			pregunta = pregunta.ToLower();
			Random random = new Random();
			int numero5 = random.Next(1, 5);
			int numero10 = random.Next(1, 10);
			
			
			if (pregunta.Contains("numero") || pregunta.Contains("del") || pregunta.Contains("entre") || pregunta.Contains("otro"))
			{
				int numeroAzar;
				// Obtener el rango especificado por el usuario
				int[] rango = ObtenerRango(pregunta);

				
				// Verificar si el rango es válido
				if (rango != null && rango.Length == 2 && rango[0] <= rango[1])
				{
					// Generar un número aleatorio dentro del rango especificado
					random = new Random();
					numeroAzar = random.Next(rango[0], rango[1] + 1); // Sumamos 1 al final para incluir el límite superior
					
					if(pregunta.Contains("numero"))
					{
						Posicion(40,7);
						Animacion("De acuerdo, aqui tienes: ");
						Color("amarillo", "negro");
						Console.WriteLine(numeroAzar);
					}
					else if(pregunta.Contains("otro"))
					{
						Posicion(40,7);
						Animacion("De acuerdo, aqui tienes otro: ");
						Color("amarillo", "negro");
						Console.WriteLine(numeroAzar);
					}
					else
					{
						Posicion(40,7);
						Animacion("Si hablas de generar un numero al azar entre esos dos");
						Posicion(40,8);
						Animacion("numeros, aqui lo tienes: ");
						Color("amarillo", "negro");
						Console.WriteLine(numeroAzar);
					}
				}
				
				else
				{
					if(preguntaAnterior.Contains("del") || preguntaAnterior.Contains("entre") || preguntaAnterior.Contains("y"))
					{
						pregunta = preguntaAnterior;
						rango = ObtenerRango(pregunta);
						numeroAzar = random.Next(rango[0], rango[1] + 1);
						
						Posicion(40,7);
						Animacion("De acuerdo, aqui tienes otro numero del "+ rango[0] + " al " + rango[1] + ": ");
						Color("amarillo", "negro");
						Console.WriteLine(numeroAzar);
					}
					else
					{
						Posicion(40,7);
						Animacion("No entendi tu pregunta.");
					}
				}
				
			}
			
			else if(pregunta.Contains("ngles") || pregunta.Contains("nglish"))
			{
				Posicion(40,7);
				Animacion("De acuerdo, cambiaremos al idioma Ingles                            ");
				
				Posicion(40,7);
				Console.WriteLine("Ready, language changed to English                   ");
				
				idioma = "english";
				
				Color("negro", "gris");
				Posicion(12,6);
				Console.WriteLine(" INFORMATION ");
				
				Posicion(12,9);
				Console.WriteLine("    USERS    ");
				
				Posicion(12,12);
				Console.WriteLine("    NEWS     ");
				
				Posicion(12,15);
				Console.WriteLine("    PLAY     ");
				
				Posicion(12,21);
				Console.WriteLine("   LOG OUT   ");
				
				Posicion(10,27);
				Console.WriteLine("ACCOUNTS    ");
				
				Color("blanco", "negro");
				Posicion(12,18);
				Console.WriteLine("   IN CHAT   ");
			}
			else if(pregunta.Contains("español") || pregunta.Contains("spanish"))
			{
				Posicion(40,7);
				Animacion("La aplicacion ya esta en Español");
			}
			
			else if(pregunta.Contains("ol") || pregunta.Contains("diamond"))
			{
				if(!preguntaAnterior.Contains("ol") && !preguntaAnterior.Contains("diam") && preguntaAnterior != "")
				{
					chats = 1;
				}
				
				
				if(chats == 1)
				{
					Posicion(40,7);
					Animacion("Hola, soy Diamond, una simulacion de IA");
					Posicion(40,8);
					Animacion("Con funciones basicas pero muy variadas,");
					Posicion(40,9);
					Animacion("¿que te gustaria hacer?");
				}
				if(chats == 2)
				{
					Posicion(40,7);
					Animacion("Hola " + usuario + ", soy Diamond, una simulacion de IA");
					Posicion(40,8);
					Animacion("Con funciones basicas pero muy variadas,");
					Posicion(40,9);
					Animacion("dime ¿en que puedo ayudarte?");
				}
				if(chats == 3)
				{
					Posicion(40,7);
					Animacion("Hola, de nuevo, por favor dime");
					Posicion(40,8);
					Animacion("¿que te gustaria hacer?");
				}
				if(chats == 4)
				{
					Posicion(40,7);
					Animacion("Creo que ya nos hemos saludado bastante");
					Posicion(40,8);
					Animacion("ja,ja,ja, ahora si dime ¿para que soy bueno?");
				}
				if(chats == 5)
				{
					Posicion(40,7);
					Animacion("OK.. esto ya empieza a ser extraño");
				}
				if(chats == 6)
				{
					Posicion(40,7);
					Animacion("Empiezas a ser molesto,");
					Posicion(40,8);
					Animacion("Ya no te saludare mas UnU");
				}
				if(chats >= 7)
				{
					Posicion(40,7);
					Animacion("...");
				}
			}
			
			else if(pregunta.Contains("chatg"))
			{
				Posicion(40,7);
				Animacion("No soy chat-GPT, soy Diamond-GPT ☻");
			}
			
			else if(preguntaAnterior.Contains("ol") && pregunta.Contains("lo siento") || preguntaAnterior.Contains("ol") && pregunta.Contains("perdon"))
			{
				Posicion(40,7);
				Animacion("Descuida");
				
				chats = 0;
			}
			
			else if(pregunta.Contains("que ") && pregunta.Contains(" hacer") || pregunta.Contains("que funciones t") || pregunta.Contains("us capacidades"))
			{
				Posicion(40,7);
				Animacion("Por el momento solo puedo ayudarte con lo siguiente:");
				
				Posicion(40,9);
				Animacion("- operaciones matematicas simples");
				Posicion(40,10);
				Animacion("- saber el dia y la hora actual");
				Posicion(40,11);
				Animacion("- pensar en un numero al azar");
				Posicion(40,12);
				Animacion("- promediar tus notas");
				Posicion(40,13);
				Animacion("- memorizar tu pregunta anterior");
				Posicion(40,14);
				Animacion("- cambiar el idioma de la aplicacion");
				Posicion(40,15);
				Animacion("- tener la mayor coherencia posible en la converzacion");
				
				chats = 0;
			}
			
			else if(pregunta.Contains("fecha") || pregunta.Contains("hoy") || pregunta.Contains(" dia"))
			{
				Posicion(40,7);
				Animacion("Hoy es " + DateTime.Today.ToString("dddd, dd MMMM yyyy"));
				
			}
			
			else if(pregunta.Contains("hora"))
			{
				Posicion(40,7);
				Animacion("Son las " + DateTime.Now.ToString("hh:mm tt"));
				
			}
			
			else if(pregunta.Contains("+") || pregunta.Contains("-") || pregunta.Contains("*") || pregunta.Contains("/") || pregunta.Contains("("))
			{

				if(pregunta.Contains("resuelve") || pregunta.Contains("cuanto") || pregunta.Contains("es") || pregunta.Contains("y"))
				{
					if(numero5 == 1)
					{
						pregunta = Regex.Replace(pregunta, @"(\d+)\(", "$1*(");
						pregunta = Regex.Replace(pregunta, @"\)(\d+)", ")*$1");
						pregunta = Regex.Replace(pregunta, @"[^\d+\-*/^()\s]", "");
						
						DataTable dataTable = new DataTable();
						var resultado = dataTable.Compute(pregunta, "");
						
						Posicion(40,7);
						Animacion("La respuesta es " + resultado.ToString());
					}
					if(numero5 == 2)
					{
						pregunta = Regex.Replace(pregunta, @"(\d+)\(", "$1*(");
						pregunta = Regex.Replace(pregunta, @"\)(\d+)", ")*$1");
						pregunta = Regex.Replace(pregunta, @"[^\d+\-*/^()\s]", "");
						
						DataTable dataTable = new DataTable();
						var resultado = dataTable.Compute(pregunta, "");
						
						Posicion(40,7);
						Animacion("Eso es " + resultado.ToString());
					}
					if(numero5 == 3)
					{
						pregunta = Regex.Replace(pregunta, @"(\d+)\(", "$1*(");
						pregunta = Regex.Replace(pregunta, @"\)(\d+)", ")*$1");
						pregunta = Regex.Replace(pregunta, @"[^\d+\-*/^()\s]", "");
						
						DataTable dataTable = new DataTable();
						var resultado = dataTable.Compute(pregunta, "");
						
						Posicion(40,7);
						Animacion("El resultado es " + resultado.ToString());
					}
					if(numero5 == 4)
					{
						pregunta = Regex.Replace(pregunta, @"(\d+)\(", "$1*(");
						pregunta = Regex.Replace(pregunta, @"\)(\d+)", ")*$1");
						pregunta = Regex.Replace(pregunta, @"[^\d+\-*/^()\s]", "");
						
						DataTable dataTable = new DataTable();
						var resultado = dataTable.Compute(pregunta, "");
						
						Posicion(40,7);
						Animacion("Es " + resultado.ToString());
					}
					if(numero5 == 5)
					{
						pregunta = Regex.Replace(pregunta, @"(\d+)\(", "$1*(");
						pregunta = Regex.Replace(pregunta, @"\)(\d+)", ")*$1");
						pregunta = Regex.Replace(pregunta, @"[^\d+\-*/^()\s]", "");
						
						DataTable dataTable = new DataTable();
						var resultado = dataTable.Compute(pregunta, "");
						
						Posicion(40,7);
						Animacion("Da " + resultado.ToString());
					}
				}
				else
				{
					pregunta = Regex.Replace(pregunta, @"(\d+)\(", "$1*(");
					pregunta = Regex.Replace(pregunta, @"\)(\d+)", ")*$1");
					pregunta = Regex.Replace(pregunta, @"[^\d+\-*/^()\s]", "");
					
					DataTable dataTable = new DataTable();
					var resultado = dataTable.Compute(pregunta, "");
					
					Posicion(40,7);
					Animacion(resultado.ToString());
				}
			}
			
			else if(pregunta.Contains("promedi") || pregunta.Contains("saque") || chats == 665 || (pregunta.Contains("ahora") && preguntaAnterior.Contains("promedia ")) || (pregunta.Contains("y") && preguntaAnterior != ""))
			{
				MatchCollection matches = Regex.Matches(pregunta, @"\d+");
				
				if((pregunta.Contains("promedia") && matches.Count != 0) || (pregunta.Contains("saque") && matches.Count != 0) || (pregunta.Contains("ahora") && matches.Count != 0) || (pregunta.Contains("y") && matches.Count != 0))
				{
					pregunta = pregunta.ToLower();
					promedio = matches.Cast<Match>().Select(m => double.Parse(m.Value)).Average();
					promedio = Math.Round(promedio, 1); // Redondear el promedio a 1 decimal
					
					Posicion(40, 7);
					Animacion("La nota es " + promedio.ToString("N1"));
					
					chats = 1;
				}
				else if(matches.Count != 0 && chats == 665)
				{
					pregunta = pregunta.ToLower();
					promedio = matches.Cast<Match>().Select(m => double.Parse(m.Value)).Average();
					promedio = Math.Round(promedio, 1); // Redondear el promedio a 1 decimal
					
					Posicion(40, 7);
					Animacion("Sacaste " + promedio.ToString("N1"));
					
					chats = 1;
				}
				else if(pregunta.Contains("saque") && matches.Count == 0)
				{
					Posicion(40, 7);
					Animacion("Ya lo veremos, proporcioname tus notas");
					
					chats = 664;
				}
				else
				{
					Posicion(40, 7);
					Animacion("Sin resultados.");
				}
			}
			
			else if(pregunta.Contains("fue") && chats == 2)
			{
				
				if(promedio < 3)
				{
					Posicion(40,7);
					Animacion("No te fue muy bien, echale mas ganas");
				}
				else if(promedio >= 3 && promedio < 6)
				{
					Posicion(40,7);
					Animacion(promedio.ToString() + " es una nota aprovatoria, aunque por supuesto");
					Posicion(40,8);
					Animacion("la puedes mejorar ;)");
				}
				else if(promedio > 30 && promedio < 60)
				{
					Posicion(40,7);
					Animacion(promedio.ToString() + " es una nota aprovatoria, aunque por supuesto");
					Posicion(40,8);
					Animacion("la puedes mejorar ;)");
				}
				else if(promedio < 30 && promedio > 9)
				{
					Posicion(40,7);
					Animacion("No te fue muy bien, echale mas ganas");
				}
				
				chats = 1;
			}
			
			else if(pregunta.Contains("resuelve") || pregunta.Contains("cuanto"))
			{
				Posicion(40,7);
				Animacion("De acuerdo, estoy listo para resulver ;)");
			}
			
			else if(pregunta.Contains("gracias") || pregunta.Contains("agrade"))
			{
				if(chats > 1)
				{
					Posicion(40,7);
					Animacion("No hay de que");
				}
				else
				{
					Posicion(40,7);
					Animacion("Aun no me preguntas nada");
				}
				chats = 0;
			}
			
			
			else
			{
				Posicion(40,7);
				Animacion("No entendi tu pregunta.");
				
				chats = 0;
			}
			Color("blanco", "negro");
		}
		
		static void RespuestasEnglish()
		{
			pregunta = pregunta.ToLower();
			Random random = new Random();
			int numero5 = random.Next(1, 5);
			int numero10 = random.Next(1, 10);

			if (pregunta.Contains("number") || pregunta.Contains("from") || pregunta.Contains("between") || pregunta.Contains("another"))
			{
				int numeroAzar;
				int[] rango = ObtenerRango(pregunta);

				if (rango != null && rango.Length == 2 && rango[0] <= rango[1])
				{
					random = new Random();
					numeroAzar = random.Next(rango[0], rango[1] + 1);

					if(pregunta.Contains("number"))
					{
						Posicion(40, 7);
						Animacion("Okay, here you go: ");
						Color("amarillo", "negro");
						Console.WriteLine(numeroAzar);
					}
					else if(pregunta.Contains("another"))
					{
						Posicion(40, 7);
						Animacion("Okay, here is another one: ");
						Color("amarillo", "negro");
						Console.WriteLine(numeroAzar);
					}
					else
					{
						Posicion(40, 7);
						Animacion("If you're talking about generating a random number between those two");
						Posicion(40, 8);
						Animacion("numbers, here it is: ");
						Color("amarillo", "negro");
						Console.WriteLine(numeroAzar);
					}
				}
				
				else
				{
					if(preguntaAnterior.Contains("from") || preguntaAnterior.Contains("between") || preguntaAnterior.Contains("and"))
					{
						pregunta = preguntaAnterior;
						rango = ObtenerRango(pregunta);
						numeroAzar = random.Next(rango[0], rango[1] + 1);

						Posicion(40, 7);
						Animacion("Okay, here is another number from "+ rango[0] + " to " + rango[1] + ": ");
						Color("amarillo", "negro");
						Console.WriteLine(numeroAzar);
					}
					else
					{
						Posicion(40, 7);
						Animacion("I didn't understand your question.");
					}
				}
			}
			
			else if(pregunta.Contains("español") || pregunta.Contains("spani"))
			{
				Posicion(40,7);
				Animacion("Okay, we will switch to Spanish language                       ");
				
				Posicion(40,7);
				Console.WriteLine("listo, cambiamos al idioma Español                     ");
				
				idioma = "español";
				
				Color("negro", "gris");
				Posicion(12,6);
				Console.WriteLine(" INFORMACION ");
				
				Posicion(12,9);
				Console.WriteLine("   USUARIOS   ");
				
				Posicion(12,12);
				Console.WriteLine("   NOTICIAS  ");
				
				Posicion(12,15);
				Console.WriteLine("    JUGAR    ");
				
				Posicion(12,21);
				Console.WriteLine("CERRAR SESION");
				
				Posicion(10,27);
				Console.WriteLine("CUENTAS    ");
				
				Color("blanco", "negro");
				Posicion(12,18);
				Console.WriteLine("   EN CHAT   ");
			}
			
			else if(pregunta.Contains("ingles") || pregunta.Contains("english"))
			{
				Posicion(40,7);
				Animacion("The application is now in English");
			}

			else if(pregunta.Contains("average") || pregunta.Contains("calculate") || chats == 665 || pregunta.Contains("now") && preguntaAnterior.Contains("average"))
			{
				MatchCollection matches = Regex.Matches(pregunta, @"\d+");

				if(pregunta.Contains("average") && matches.Count != 0 || pregunta.Contains("calculate") && matches.Count != 0  || pregunta.Contains("now") && matches.Count != 0 || pregunta.Contains("and") && matches.Count != 0)
				{
					pregunta = pregunta.ToLower();
					promedio = matches.Cast<Match>().Select(m => double.Parse(m.Value)).Average();

					Posicion(40, 7);
					Animacion("The grade is " + promedio.ToString());

					chats = 1;
				}
				else if(matches.Count != 0 && chats == 665)
				{
					pregunta = pregunta.ToLower();
					promedio = matches.Cast<Match>().Select(m => double.Parse(m.Value)).Average();

					Posicion(40, 7);
					Animacion("You got " + promedio.ToString());

					chats = 1;
				}
				else if(pregunta.Contains("calculate") && matches.Count == 0)
				{
					Posicion(40, 7);
					Animacion("We'll see, give me your grades");

					chats = 664;
				}
				else
				{
					Posicion(40, 7);
					Animacion("No results.");
				}
			}

			else if(pregunta.Contains("chatg"))
			{
				Posicion(40,7);
				Animacion("I am not Chat-GPT, I am Diamond-GPT ☻");
			}
			
			else if(pregunta.Contains("date") || pregunta.Contains("today") || pregunta.Contains("day"))
			{
				Posicion(40, 7);
				Animacion("Today is " + DateTime.Today.ToString("dddd, dd MMMM yyyy"));
			}

			else if(pregunta.Contains("time"))
			{
				Posicion(40, 7);
				Animacion("The time is " + DateTime.Now.ToString("hh:mm tt"));
			}

			else if(pregunta.Contains("hi") || pregunta.Contains("diamond") || pregunta.Contains("hello"))
			{
				if(!preguntaAnterior.Contains("hi") && !preguntaAnterior.Contains("he") && !preguntaAnterior.Contains("diamond") && preguntaAnterior != "")
				{
					chats = 1;
				}

				if(chats == 1)
				{
					Posicion(40, 7);
					Animacion("Hello, I am Diamond, an AI simulation");
					Posicion(40, 8);
					Animacion("With basic but very varied functions,");
					Posicion(40, 9);
					Animacion("what would you like to do?");
				}
				if(chats == 2)
				{
					Posicion(40, 7);
					Animacion("Hello " + usuario + ", I am Diamond, an AI simulation");
					Posicion(40, 8);
					Animacion("With basic but very varied functions,");
					Posicion(40, 9);
					Animacion("tell me, how can I help you?");
				}
				if(chats == 3)
				{
					Posicion(40, 7);
					Animacion("Hello again, please tell me");
					Posicion(40, 8);
					Animacion("what would you like to do?");
				}
				if(chats == 4)
				{
					Posicion(40, 7);
					Animacion("I think we've greeted enough");
					Posicion(40, 8);
					Animacion("ha, ha, ha, now tell me, what can I do for you?");
				}
				if(chats == 5)
				{
					Posicion(40, 7);
					Animacion("OK.. this is starting to get strange");
				}
				if(chats == 6)
				{
					Posicion(40, 7);
					Animacion("You're starting to be annoying,");
					Posicion(40, 8);
					Animacion("I won't greet you anymore UnU");
				}
				if(chats >= 7)
				{
					Posicion(40, 7);
					Animacion("...");
				}
			}

			else if(preguntaAnterior.Contains("hi") && (pregunta.Contains("sorry") || pregunta.Contains("apologize")))
			{
				Posicion(40, 7);
				Animacion("Don't worry");

				chats = 0;
			}

			else if(pregunta.Contains("what ") && (pregunta.Contains("do") || pregunta.Contains("can you do") || pregunta.Contains("your capabilities")))
			{
				Posicion(40, 7);
				Animacion("For now, I can only help you with the following:");

				Posicion(40, 9);
				Animacion("- simple math operations");
				Posicion(40, 10);
				Animacion("- knowing the current date and time");
				Posicion(40, 11);
				Animacion("- thinking of a random number");
				Posicion(40, 12);
				Animacion("- averaging your grades");
				Posicion(40, 13);
				Animacion("- memorizing your previous question");
				Posicion(40,14);
				Animacion("- change the language of the application");
				Posicion(40, 15);
				Animacion("- having the most coherent conversation possible");

				chats = 0;
			}

			else if(pregunta.Contains("+") || pregunta.Contains("-") || pregunta.Contains("*") || pregunta.Contains("/") || pregunta.Contains("("))
			{
				if(pregunta.Contains("solve") || pregunta.Contains("how much") || pregunta.Contains("is") || pregunta.Contains("and"))
				{
					if(numero5 == 1)
					{
						pregunta = Regex.Replace(pregunta, @"(\d+)\(", "$1*(");
						pregunta = Regex.Replace(pregunta, @"\)(\d+)", ")*$1");
						pregunta = Regex.Replace(pregunta, @"[^\d+\-*/^()\s]", "");

						DataTable dataTable = new DataTable();
						var resultado = dataTable.Compute(pregunta, "");

						Posicion(40, 7);
						Animacion("The answer is " + resultado.ToString());
					}
					if(numero5 == 2)
					{
						pregunta = Regex.Replace(pregunta, @"(\d+)\(", "$1*(");
						pregunta = Regex.Replace(pregunta, @"\)(\d+)", ")*$1");
						pregunta = Regex.Replace(pregunta, @"[^\d+\-*/^()\s]", "");

						DataTable dataTable = new DataTable();
						var resultado = dataTable.Compute(pregunta, "");

						Posicion(40, 7);
						Animacion("That's " + resultado.ToString());
					}
					if(numero5 == 3)
					{
						pregunta = Regex.Replace(pregunta, @"(\d+)\(", "$1*(");
						pregunta = Regex.Replace(pregunta, @"\)(\d+)", ")*$1");
						pregunta = Regex.Replace(pregunta, @"[^\d+\-*/^()\s]", "");

						DataTable dataTable = new DataTable();
						var resultado = dataTable.Compute(pregunta, "");

						Posicion(40, 7);
						Animacion("The result is " + resultado.ToString());
					}
					if (numero5 == 4)
					{
						pregunta = Regex.Replace(pregunta, @"(\d+)\(", "$1*(");
						pregunta = Regex.Replace(pregunta, @"\)(\d+)", ")*$1");
						pregunta = Regex.Replace(pregunta, @"[^\d+\-*/^()\s]", "");

						DataTable dataTable = new DataTable();
						var resultado = dataTable.Compute(pregunta, "");

						Posicion(40, 7);
						Animacion("It is " + resultado.ToString());
					}
					if (numero5 == 5)
					{
						pregunta = Regex.Replace(pregunta, @"(\d+)\(", "$1*(");
						pregunta = Regex.Replace(pregunta, @"\)(\d+)", ")*$1");
						pregunta = Regex.Replace(pregunta, @"[^\d+\-*/^()\s]", "");

						DataTable dataTable = new DataTable();
						var resultado = dataTable.Compute(pregunta, "");

						Posicion(40, 7);
						Animacion("It gives " + resultado.ToString());
					}
				}
				else
				{
					pregunta = Regex.Replace(pregunta, @"(\d+)\(", "$1*(");
					pregunta = Regex.Replace(pregunta, @"\)(\d+)", ")*$1");
					pregunta = Regex.Replace(pregunta, @"[^\d+\-*/^()\s]", "");

					DataTable dataTable = new DataTable();
					var resultado = dataTable.Compute(pregunta, "");

					Posicion(40, 7);
					Animacion(resultado.ToString());
				}
				chats = 0;
			}
			else if (pregunta.Contains("was") && chats == 2)
			{

				if (promedio < 3)
				{
					Posicion(40, 7);
					Animacion("You didn't do very well, try harder");
				}
				else if (promedio >= 3 && promedio < 6)
				{
					Posicion(40, 7);
					Animacion(promedio.ToString() + " is a passing grade, but of course");
					Posicion(40, 8);
					Animacion("you can improve it ;)");
				}
				else if (promedio > 30 && promedio < 60)
				{
					Posicion(40, 7);
					Animacion(promedio.ToString() + " is a passing grade, but of course");
					Posicion(40, 8);
					Animacion("you can improve it ;)");
				}
				else if (promedio < 30 && promedio > 9)
				{
					Posicion(40, 7);
					Animacion("You didn't do very well, try harder");
				}

				chats = 1;
			}
			else if (pregunta.Contains("solve") || pregunta.Contains("how much"))
			{
				Posicion(40, 7);
				Animacion("Okay, I'm ready to solve ;)");
			}
			else if (pregunta.Contains("thank you") || pregunta.Contains("thanks"))
			{
				if (chats > 1)
				{
					Posicion(40, 7);
					Animacion("You're welcome");
					chats = 0;
				}
				else
				{
					Posicion(40, 7);
					Animacion("You haven't asked me anything yet");
				}
			}
			
			else
			{
				Posicion(40,7);
				Animacion("I didn't understand your question.");

				chats = 0;
			}

			Color("blanco", "negro");
		}
		
		static void Juego1()
		{
			Random random = new Random();
			bool jug1 = true;
			bool jug2 = false;

			// Generar un valor booleano aleatorio
			bool temp = random.Next(0, 2) == 0;

			// Intercambiar los valores
			if (temp)
			{
				bool tempValue = jug1;
				jug1 = jug2;
				jug2 = tempValue;
			}
			
			bool salir_juego1 = false;
			
			int pos1 = 1;
			int pos2 = 1;
			
			int movimientos1 = 0;
			int movimientos2 = 0;
			
			int victorias1 = 0;
			int victorias2 = 0;
			
			int nuevosPuntos = 0;
			
			int rondas = 1;
			
			int terminar = 0;
			
			bool marcar1 = false;
			bool marcar2 = false;
			bool marcar3 = false;
			bool marcar4 = false;
			bool marcar5 = false;
			bool marcar6 = false;
			bool marcar7 = false;
			bool marcar8 = false;
			bool marcar9 = false;
			
			bool Marcar1 = false;
			bool Marcar2 = false;
			bool Marcar3 = false;
			bool Marcar4 = false;
			bool Marcar5 = false;
			bool Marcar6 = false;
			bool Marcar7 = false;
			bool Marcar8 = false;
			bool Marcar9 = false;
			
			opcion = 1;
			
			Color("blanco", "negro");
			for (fila = 2; fila <=27; fila++) {
				for (columna = 36; columna <= 115; columna++) {
					Posicion(columna, fila);
					Console.WriteLine(" ");
				}
			}
			
			
			do
			{
				Color("negro", "blanco");
				columna = 69; //lado iskierdo
				for( fila = 8; fila<=21; fila=fila+1 ){
					Posicion(columna, fila);Console.WriteLine(" ");
				}
				
				columna = 81; //lado iskierdo
				for( fila = 8; fila<=21; fila=fila+1 ){
					Posicion(columna, fila);Console.WriteLine(" ");
				}
				
				fila = 12;
				for( columna = 60; columna<=90; columna=columna+1 ){
					Posicion(columna, fila);Console.WriteLine("▄");
				}
				
				fila = 17;
				for( columna = 60; columna<=90; columna=columna+1 ){
					Posicion(columna, fila);Console.WriteLine("▄");
				}
				
				
				Color("negro", "negro");
				for (fila = 9; fila <=10; fila++) {
					for (columna = 62; columna <= 66; columna++) {
						Posicion(columna, fila);
						Console.WriteLine(" ");
					}
				}

				for (fila = 9; fila <=10; fila++) {
					for (columna = 73; columna <= 77; columna++) {
						Posicion(columna, fila);
						Console.WriteLine(" ");
					}
				}

				for (fila = 9; fila <=10; fila++) {
					for (columna = 84; columna <= 88; columna++) {
						Posicion(columna, fila);
						Console.WriteLine(" ");
					}
				}

				for (fila = 14; fila <=15; fila++) {
					for (columna = 62; columna <= 66; columna++) {
						Posicion(columna, fila);
						Console.WriteLine(" ");
					}
				}

				for (fila = 14; fila <=15; fila++) {
					for (columna = 73; columna <= 77; columna++) {
						Posicion(columna, fila);
						Console.WriteLine(" ");
					}
				}

				for (fila = 14; fila <=15; fila++) {
					for (columna = 84; columna <= 88; columna++) {
						Posicion(columna, fila);
						Console.WriteLine(" ");
					}
				}

				for (fila = 19; fila <=20; fila++) {
					for (columna = 62; columna <= 66; columna++) {
						Posicion(columna, fila);
						Console.WriteLine(" ");
					}
				}

				for (fila = 19; fila <=20; fila++) {
					for (columna = 73; columna <= 77; columna++) {
						Posicion(columna, fila);
						Console.WriteLine(" ");
					}
				}

				for (fila = 19; fila <=20; fila++) {
					for (columna = 84; columna <= 88; columna++) {
						Posicion(columna, fila);
						Console.WriteLine(" ");
					}
				}
				
				
				Color("negro", "verde");
				
				if(marcar1 == true)
				{
					
					for (fila = 9; fila <=10; fila++) {
						for (columna = 62; columna <= 66; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(marcar2 == true)
				{
					for (fila = 9; fila <=10; fila++) {
						for (columna = 73; columna <= 77; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(marcar3 == true)
				{
					for (fila = 9; fila <=10; fila++) {
						for (columna = 84; columna <= 88; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(marcar4 == true)
				{
					for (fila = 14; fila <=15; fila++) {
						for (columna = 62; columna <= 66; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(marcar5 == true)
				{
					for (fila = 14; fila <=15; fila++) {
						for (columna = 73; columna <= 77; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(marcar6 == true)
				{
					for (fila = 14; fila <=15; fila++) {
						for (columna = 84; columna <= 88; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(marcar7 == true)
				{
					for (fila = 19; fila <=20; fila++) {
						for (columna = 62; columna <= 66; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(marcar8 == true)
				{
					for (fila = 19; fila <=20; fila++) {
						for (columna = 73; columna <= 77; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(marcar9 == true)
				{
					for (fila = 19; fila <=20; fila++) {
						for (columna = 84; columna <= 88; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				Color("negro", "rojo");
				
				if(Marcar1 == true)
				{
					for (fila = 9; fila <=10; fila++) {
						for (columna = 62; columna <= 66; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				if(Marcar2 == true)
				{
					for (fila = 9; fila <=10; fila++) {
						for (columna = 73; columna <= 77; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(Marcar3 == true)
				{
					for (fila = 9; fila <=10; fila++) {
						for (columna = 84; columna <= 88; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(Marcar4 == true)
				{
					for (fila = 14; fila <=15; fila++) {
						for (columna = 62; columna <= 66; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(Marcar5 == true)
				{
					for (fila = 14; fila <=15; fila++) {
						for (columna = 73; columna <= 77; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(Marcar6 == true)
				{
					for (fila = 14; fila <=15; fila++) {
						for (columna = 84; columna <= 88; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(Marcar7 == true)
				{
					for (fila = 19; fila <=20; fila++) {
						for (columna = 62; columna <= 66; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(Marcar8 == true)
				{
					for (fila = 19; fila <=20; fila++) {
						for (columna = 73; columna <= 77; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(Marcar9 == true)
				{
					for (fila = 19; fila <=20; fila++) {
						for (columna = 84; columna <= 88; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(mostrar_movimientos == true)
				{
					if(idioma == "español")
					{
						Color("blanco", "negro");
						Posicion(38,3);
						Console.WriteLine("MOVIMIENTOS: ");
					}
					if(idioma == "english")
					{
						Color("blanco", "negro");
						Posicion(38,3);
						Console.WriteLine("MOVEMENTS: ");

					}
					
					Color("verde", "negro");
					Posicion(51,3);
					Console.WriteLine(movimientos1);
					
					Color("rojo", "negro");
					Posicion(51,4);
					Console.WriteLine(movimientos2);
				}
				
				if(mostrar_pos == true)
				{
					if(idioma == "español")
					{
						Color("blanco", "negro");
						Posicion(98,3);
						Console.WriteLine("POS. CASILLA: ");
					}
					if(idioma == "english")
					{
						Color("blanco", "negro");
						Posicion(98,3);
						Console.WriteLine("POSITION: ");
					}
					
					Color("cian", "negro");
					Posicion(112,3);
					Console.WriteLine(pos1);
				}
				
				if(mostrar_victorias == true)
				{
					if(idioma == "español")
					{
						Color("blanco", "negro");
						Posicion(38,24);
						Console.WriteLine("VICTORIAS: ");
						
						Color("verde", "negro");
						Posicion(50,24);
						Console.WriteLine(victorias1);
						
						Color("rojo", "negro");
						Posicion(50,25);
						Console.WriteLine(victorias2);
					}
					if(idioma == "english")
					{
						Color("blanco", "negro");
						Posicion(38,24);
						Console.WriteLine("WINS: ");
						
						Color("verde", "negro");
						Posicion(44,24);
						Console.WriteLine(victorias1);
						
						Color("rojo", "negro");
						Posicion(44,25);
						Console.WriteLine(victorias2);
					}
				}
				
				if(mostrar_rondas == true)
				{
					if(idioma == "español")
					{
						Color("negro", "amarillo");
						Posicion(71,2);
						Console.WriteLine(" RONDA " + rondas +" ");
					}
					if(idioma == "english")
					{
						Color("negro", "amarillo");
						Posicion(71,2);
						Console.WriteLine(" ROUND " + rondas +" ");
					}
				}
				
				if(idioma == "español")
				{
					Color("amarillo", "negro");
					Posicion(102,25);
					Console.Write("AJUSTES ");
				}
				if(idioma == "english")
				{
					Color("amarillo", "negro");
					Posicion(101,25);
					Console.Write("SETTINGS ");
				}
				
				Color("negro", "amarillo");
				Console.Write(" + ");
				
				
				if(jug1 == true)
				{
					if(idioma == "español")
					{
						Color("negro", "verde");
						Posicion(36,27);
						Console.WriteLine("                                 TURNO DEL VERDE                                ");
					}
					if(idioma == "english")
					{
						Color("negro", "verde");
						Posicion(36,27);
						Console.WriteLine("                                  GREEN'S TURN                                  ");

					}
					
					Color("negro", "verdeoscuro");
				}
				else if(jug2 == true)
				{
					if(idioma == "español")
					{
						Color("negro", "rojo");
						Posicion(36,27);
						Console.WriteLine("                                 TURNO DEL ROJO                                 ");
					}
					if(idioma == "english")
					{
						Color("negro", "rojo");
						Posicion(36,27);
						Console.WriteLine("                                   RED'S TURN                                   ");

					}
					
					Color("negro", "rojooscuro");
				}
				
				if(pos1 == 1)
				{
					if(marcar1 == true || Marcar1 == true)
					{
						Color("negro", "blanco");
					}
					
					for (fila = 9; fila <=10; fila++) {
						for (columna = 62; columna <= 66; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(pos1 == 2)
				{
					if(marcar2 == true || Marcar2 == true)
					{
						Color("negro", "blanco");
					}
					
					for (fila = 9; fila <=10; fila++) {
						for (columna = 73; columna <= 77; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(pos1 == 3)
				{
					if(marcar3 == true || Marcar3 == true)
					{
						Color("negro", "blanco");
					}
					
					for (fila = 9; fila <=10; fila++) {
						for (columna = 84; columna <= 88; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(pos1 == 4)
				{
					if(marcar4 == true || Marcar4 == true)
					{
						Color("negro", "blanco");
					}
					
					for (fila = 14; fila <=15; fila++) {
						for (columna = 62; columna <= 66; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(pos1 == 5)
				{
					if(marcar5 == true || Marcar5 == true)
					{
						Color("negro", "blanco");
					}
					
					for (fila = 14; fila <=15; fila++) {
						for (columna = 73; columna <= 77; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(pos1 == 6)
				{
					if(marcar6 == true || Marcar6 == true)
					{
						Color("negro", "blanco");
					}
					
					for (fila = 14; fila <=15; fila++) {
						for (columna = 84; columna <= 88; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(pos1 == 7)
				{
					if(marcar7 == true || Marcar7 == true)
					{
						Color("negro", "blanco");
					}
					
					for (fila = 19; fila <=20; fila++) {
						for (columna = 62; columna <= 66; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(pos1 == 8)
				{
					if(marcar8 == true || Marcar8 == true)
					{
						Color("negro", "blanco");
					}
					
					for (fila = 19; fila <=20; fila++) {
						for (columna = 73; columna <= 77; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				if(pos1 == 9)
				{
					if(marcar9 == true || Marcar9 == true)
					{
						Color("negro", "blanco");
					}
					
					for (fila = 19; fila <=20; fila++) {
						for (columna = 84; columna <= 88; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
				Precionar();
				
				switch(tecla.Key)
				{
					case ConsoleKey.R:
						
						terminar = 0;
						rondas = 1;
						
						movimientos1 = 0;
						movimientos2 = 0;
						
						victorias1 = 0;
						victorias2 = 0;
						
						pos1 = 1;
						pos2 = 1;
						
						if (temp)
						{
							bool tempValue = jug1;
							jug1 = jug2;
							jug2 = tempValue;
						}
						
						marcar1 = false;
						marcar2 = false;
						marcar3 = false;
						marcar4 = false;
						marcar5 = false;
						marcar6 = false;
						marcar7 = false;
						marcar8 = false;
						marcar9 = false;
						
						Marcar1 = false;
						Marcar2 = false;
						Marcar3 = false;
						Marcar4 = false;
						Marcar5 = false;
						Marcar6 = false;
						Marcar7 = false;
						Marcar8 = false;
						Marcar9 = false;
						
						break;
						
					case ConsoleKey.Escape:
						
						salir_juego1 = true;
						break;
						
					case ConsoleKey.UpArrow:
					case ConsoleKey.W:
						
						if(pos1 > 1 && pos1 != 2 && pos1 != 3)
						{
							pos1 = pos1 - 3;
						}
						break;
						
					case ConsoleKey.DownArrow:
					case ConsoleKey.S:
						
						if(pos1 < 9 && pos1 != 8 && pos1 != 7)
						{
							pos1 = pos1 + 3;
						}
						break;
						
					case ConsoleKey.RightArrow:
					case ConsoleKey.D:
						
						if(pos1 < 9)
						{
							pos1 = pos1 + 1;
						}
						break;
						
					case ConsoleKey.LeftArrow:
					case ConsoleKey.A:
						
						if(pos1 > 1)
						{
							pos1 = pos1 - 1;
						}
						break;
						
					case ConsoleKey.Enter:
					case ConsoleKey.Spacebar:
						
						if(jug1 == true)
						{
							jug1 = false;
							jug2 = true;
							
							if(pos1 == 1 && marcar1 != true && Marcar1 != true)
							{
								marcar1 = true;
								terminar++;
								movimientos1++;
							}
							else if(pos1 == 2 && marcar2 != true && Marcar2 != true)
							{
								marcar2 = true;
								terminar++;
								movimientos1++;
							}
							else if(pos1 == 3 && marcar3 != true && Marcar3 != true)
							{
								marcar3 = true;
								terminar++;
								movimientos1++;
							}
							else if(pos1 == 4 && marcar4 != true && Marcar4 != true)
							{
								marcar4 = true;
								terminar++;
								movimientos1++;
							}
							else if(pos1 == 5 && marcar5 != true && Marcar5 != true)
							{
								marcar5 = true;
								terminar++;
								movimientos1++;
							}
							else if(pos1 == 6 && marcar6 != true && Marcar6 != true)
							{
								marcar6 = true;
								terminar++;
								movimientos1++;
							}
							else if(pos1 == 7 && marcar7 != true && Marcar7 != true)
							{
								marcar7 = true;
								terminar++;
								movimientos1++;
							}
							else if(pos1 == 8 && marcar8 != true && Marcar8 != true)
							{
								marcar8 = true;
								terminar++;
								movimientos1++;
							}
							else if(pos1 == 9 && marcar9 != true && Marcar9 != true)
							{
								marcar9 = true;
								terminar++;
								movimientos1++;
							}
							else
							{
								jug1 = true;
								jug2 = false;
							}
						}
						else if(jug2 == true)
						{
							jug1 = true;
							jug2 = false;
							
							if(pos1 == 1 && Marcar1 != true && marcar1 != true)
							{
								Marcar1 = true;
								terminar++;
								movimientos2++;
							}
							else if(pos1 == 2 && Marcar2 != true && marcar2 != true)
							{
								Marcar2 = true;
								terminar++;
								movimientos2++;
							}
							else if(pos1 == 3 && Marcar3 != true && marcar3 != true)
							{
								Marcar3 = true;
								terminar++;
								movimientos2++;
							}
							else if(pos1 == 4 && Marcar4 != true && marcar4 != true)
							{
								Marcar4 = true;
								terminar++;
								movimientos2++;
							}
							else if(pos1 == 5 && Marcar5 != true && marcar5 != true)
							{
								Marcar5 = true;
								terminar++;
								movimientos2++;
							}
							else if(pos1 == 6 && Marcar6 != true && marcar6 != true)
							{
								Marcar6 = true;
								terminar++;
								movimientos2++;
							}
							else if(pos1 == 7 && Marcar7 != true && marcar7 != true)
							{
								Marcar7 = true;
								terminar++;
								movimientos2++;
							}
							else if(pos1 == 8 && Marcar8 != true && marcar8 != true)
							{
								Marcar8 = true;
								terminar++;
								movimientos2++;
							}
							else if(pos1 == 9 && Marcar9 != true && marcar9 != true)
							{
								Marcar9 = true;
								terminar++;
								movimientos2++;
							}
							else
							{
								jug1 = false;
								jug2 = true;
							}
						}
						break;
				}
				
				//si el jugador 1 gana
				if ((marcar1 && marcar2 && marcar3) ||
				    (marcar4 && marcar5 && marcar6) ||
				    (marcar7 && marcar8 && marcar9) ||
				    (marcar1 && marcar4 && marcar7) ||
				    (marcar2 && marcar5 && marcar8) ||
				    (marcar3 && marcar6 && marcar9) ||
				    (marcar1 && marcar5 && marcar9) ||
				    (marcar3 && marcar5 && marcar7))
				{
					nuevosPuntos = 5; // Nuevos puntos que quieres asignar al usuario

					// Llama a la función para actualizar los puntos del usuario
					ActualizarPuntos("Datos.txt", usuario, nuevosPuntos);
					
					victorias1++;
					rondas++;
					
					terminar = 0;
					
					movimientos1 = 0;
					movimientos2 = 0;
					
					pos1 = 1;
					pos2 = 1;
					
					jug1 = true;
					jug2 = false;
					
					marcar1 = false;
					marcar2 = false;
					marcar3 = false;
					marcar4 = false;
					marcar5 = false;
					marcar6 = false;
					marcar7 = false;
					marcar8 = false;
					marcar9 = false;
					
					Marcar1 = false;
					Marcar2 = false;
					Marcar3 = false;
					Marcar4 = false;
					Marcar5 = false;
					Marcar6 = false;
					Marcar7 = false;
					Marcar8 = false;
					Marcar9 = false;
					
					Tiempo(50);
					
					Color("negro", "verde");
					columna = 69; //lado iskierdo
					for( fila = 8; fila<=21; fila=fila+1 ){
						Posicion(columna, fila);Console.WriteLine(" ");
					}
					
					Tiempo(50);
					
					columna = 81; //lado iskierdo
					for( fila = 8; fila<=21; fila=fila+1 ){
						Posicion(columna, fila);Console.WriteLine(" ");
					}
					
					Tiempo(50);
					
					fila = 12;
					for( columna = 60; columna<=90; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("▄");
					}
					
					Tiempo(50);
					
					fila = 17;
					for( columna = 60; columna<=90; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("▄");
					}
					
					Tiempo(500);
					
					if(pantalla_victorias == true)
					{
						Color("blanco", "negro");
						for (fila = 2; fila <=27; fila++) {
							for (columna = 36; columna <= 115; columna++) {
								Posicion(columna, fila);
								Console.WriteLine(" ");
							}
						}
						
						if(idioma == "español")
						{
							string[] arteASCII =
							{
								"██    ██ ██  ██████ ████████  ██████  ██████  ██  █████  ",
								"██    ██ ██ ██         ██    ██    ██ ██   ██ ██ ██   ██ ",
								"██    ██ ██ ██         ██    ██    ██ ██████  ██ ███████ ",
								" ██  ██  ██ ██         ██    ██    ██ ██   ██ ██ ██   ██ ",
								"  ████   ██  ██████    ██     ██████  ██   ██ ██ ██   ██ "
							};

							for (int i = 0; i < arteASCII.Length; i++)
							{
								Posicion(48, 6 + i);
								Console.WriteLine(arteASCII[i]);
							}
						}
						if(idioma == "english")
						{
							string[] arteASCII =
							{
								"██    ██ ██  ██████ ████████  ██████  ██████  ██  ██ ",
								"██    ██ ██ ██         ██    ██    ██ ██   ██ ██████ ",
								"██    ██ ██ ██         ██    ██    ██ ██████      ██   ",
								" ██  ██  ██ ██         ██    ██    ██ ██   ██ █  ███   ",
								"  ████   ██  ██████    ██     ██████  ██   ██ █████  "
							};

							for (int i = 0; i < arteASCII.Length; i++)
							{
								Posicion(48, 6 + i);
								Console.WriteLine(arteASCII[i]);
							}
						}
						
						string[] arteASCII2 =
						{
							"░░░░█▐▄▒▒▒▌▌▒▒▌░▌▒▐▐▐▒▒▐▒▒▌▒▀▄▀▄░",
							"░░░█▐▒▒▀▀▌░▀▀▀░░▀▀▀░░▀▀▄▌▌▐▒▒▒▌▐░",
							"░░▐▒▒▀▀▄▐░▀▀▄▄░░░░░░░░░░░▐▒▌▒▒▐░▌",
							"░░▐▒▌▒▒▒▌░▄▄▄▄█▄░░░░░░░▄▄▄▐▐▄▄▀░░",
							"░░▌▐▒▒▒▐░░░░░░░░░░░░░▀█▄░░░░▌▌░░░",
							"▄▀▒▒▌▒▒▐░░░░░░░▄░░▄░░░░░▀▀░░▌▌░░░",
							"▄▄▀▒▐▒▒▐░░░░░░░▐▀▀▀▄▄▀░░░░░░▌▌░░░",
							"░░░░█▌▒▒▌░░░░░▐▒▒▒▒▒▌░░░░░░▐▐▒▀▀▄",
							"░░▄▀▒▒▒▒▐░░░░░▐▒▒▒▒▐░░░░░▄█▄▒▐▒▒▒",
							"▄▀▒▒▒▒▒▄██▀▄▄░░▀▄▄▀░░▄▄▀█▄░█▀▒▒▒▒"
						};

						for (int i = 0; i < arteASCII2.Length; i++)
						{
							Color("negro", "verde");
							Posicion(60, 14 + i);
							Console.WriteLine(arteASCII2[i]);
						}
						
						if(idioma == "español")
						{
							Color("verde", "negro");
							Posicion(65,11);
							Console.WriteLine(usuario + " HA GANADO " + nuevosPuntos + " PUNTOS");
							
							Color("negro", "blanco");
							Posicion(36,26);
							Console.WriteLine("                    Preciona cualquier tecla para continuar                     ");
						}
						if(idioma == "english")
						{
							Color("verde", "negro");
							Posicion(65,11);
							Console.WriteLine(usuario + " HAS WON "  + nuevosPuntos + " POINTS");

							Color("negro", "blanco");
							Posicion(36,26);
							Console.WriteLine("                          Press any key to continue                             ");
						}
						
						Precionar();
						
						Color("blanco", "negro");
						for (fila = 2; fila <=27; fila++) {
							for (columna = 36; columna <= 115; columna++) {
								Posicion(columna, fila);
								Console.WriteLine(" ");
							}
						}
					}
				}
				//si el jugador 2 gana
				else if ((Marcar1 && Marcar2 && Marcar3) ||
				         (Marcar4 && Marcar5 && Marcar6) ||
				         (Marcar7 && Marcar8 && Marcar9) ||
				         (Marcar1 && Marcar4 && Marcar7) ||
				         (Marcar2 && Marcar5 && Marcar8) ||
				         (Marcar3 && Marcar6 && Marcar9) ||
				         (Marcar1 && Marcar5 && Marcar9) ||
				         (Marcar3 && Marcar5 && Marcar7))
				{
					victorias2++;
					rondas++;
					
					terminar = 0;
					
					movimientos1 = 0;
					movimientos2 = 0;
					
					pos1 = 1;
					pos2 = 1;
					
					jug1 = false;
					jug2 = true;
					
					marcar1 = false;
					marcar2 = false;
					marcar3 = false;
					marcar4 = false;
					marcar5 = false;
					marcar6 = false;
					marcar7 = false;
					marcar8 = false;
					marcar9 = false;
					
					Marcar1 = false;
					Marcar2 = false;
					Marcar3 = false;
					Marcar4 = false;
					Marcar5 = false;
					Marcar6 = false;
					Marcar7 = false;
					Marcar8 = false;
					Marcar9 = false;
					
					Tiempo(50);
					
					Color("negro", "rojo");
					columna = 69; //lado iskierdo
					for( fila = 8; fila<=21; fila=fila+1 ){
						Posicion(columna, fila);Console.WriteLine(" ");
					}
					
					Tiempo(50);
					
					columna = 81; //lado iskierdo
					for( fila = 8; fila<=21; fila=fila+1 ){
						Posicion(columna, fila);Console.WriteLine(" ");
					}
					
					Tiempo(50);
					
					fila = 12;
					for( columna = 60; columna<=90; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("▄");
					}
					
					Tiempo(50);
					
					fila = 17;
					for( columna = 60; columna<=90; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("▄");
					}
					
					Tiempo(500);
					
					if(pantalla_victorias == true)
					{
						Color("blanco", "negro");
						for (fila = 2; fila <=27; fila++) {
							for (columna = 36; columna <= 115; columna++) {
								Posicion(columna, fila);
								Console.WriteLine(" ");
							}
						}
						
						if(idioma == "español")
						{
							string[] arteASCII =
							{
								"██    ██ ██  ██████ ████████  ██████  ██████  ██  █████  ",
								"██    ██ ██ ██         ██    ██    ██ ██   ██ ██ ██   ██ ",
								"██    ██ ██ ██         ██    ██    ██ ██████  ██ ███████ ",
								" ██  ██  ██ ██         ██    ██    ██ ██   ██ ██ ██   ██ ",
								"  ████   ██  ██████    ██     ██████  ██   ██ ██ ██   ██ "
							};

							for (int i = 0; i < arteASCII.Length; i++)
							{
								Posicion(48, 6 + i);
								Console.WriteLine(arteASCII[i]);
							}
						}
						if(idioma == "english")
						{
							string[] arteASCII =
							{
								"██    ██ ██  ██████ ████████  ██████  ██████  ██  ██ ",
								"██    ██ ██ ██         ██    ██    ██ ██   ██ ██████ ",
								"██    ██ ██ ██         ██    ██    ██ ██████      ██   ",
								" ██  ██  ██ ██         ██    ██    ██ ██   ██ █  ███   ",
								"  ████   ██  ██████    ██     ██████  ██   ██ █████  "
							};

							for (int i = 0; i < arteASCII.Length; i++)
							{
								Posicion(48, 6 + i);
								Console.WriteLine(arteASCII[i]);
							}
						}
						
						string[] arteASCII2 =
						{
							"░░░░█▐▄▒▒▒▌▌▒▒▌░▌▒▐▐▐▒▒▐▒▒▌▒▀▄▀▄░",
							"░░░█▐▒▒▀▀▌░▀▀▀░░▀▀▀░░▀▀▄▌▌▐▒▒▒▌▐░",
							"░░▐▒▒▀▀▄▐░▀▀▄▄░░░░░░░░░░░▐▒▌▒▒▐░▌",
							"░░▐▒▌▒▒▒▌░▄▄▄▄█▄░░░░░░░▄▄▄▐▐▄▄▀░░",
							"░░▌▐▒▒▒▐░░░░░░░░░░░░░▀█▄░░░░▌▌░░░",
							"▄▀▒▒▌▒▒▐░░░░░░░▄░░▄░░░░░▀▀░░▌▌░░░",
							"▄▄▀▒▐▒▒▐░░░░░░░▐▀▀▀▄▄▀░░░░░░▌▌░░░",
							"░░░░█▌▒▒▌░░░░░▐▒▒▒▒▒▌░░░░░░▐▐▒▀▀▄",
							"░░▄▀▒▒▒▒▐░░░░░▐▒▒▒▒▐░░░░░▄█▄▒▐▒▒▒",
							"▄▀▒▒▒▒▒▄██▀▄▄░░▀▄▄▀░░▄▄▀█▄░█▀▒▒▒▒"
						};

						for (int i = 0; i < arteASCII2.Length; i++)
						{
							Color("negro", "rojo");
							Posicion(60, 14 + i);
							Console.WriteLine(arteASCII2[i]);
						}
						
						if(idioma == "español")
						{
							Color("rojo", "negro");
							Posicion(67,11);
							Console.WriteLine("EL ROJO HA GANADO");
							
							Color("negro", "blanco");
							Posicion(36,26);
							Console.WriteLine("                    Preciona cualquier tecla para continuar                     ");
						}
						if(idioma == "english")
						{
							Color("rojo", "negro");
							Posicion(67,11);
							Console.WriteLine("RED HAS WON");

							Color("negro", "blanco");
							Posicion(36,26);
							Console.WriteLine("            Press any key to continue            ");
						}
						
						Precionar();
						
						Color("blanco", "negro");
						for (fila = 2; fila <=27; fila++) {
							for (columna = 36; columna <= 115; columna++) {
								Posicion(columna, fila);
								Console.WriteLine(" ");
							}
						}
					}
				}
				
				else if(terminar == 9)
				{
					rondas++;
					
					terminar = 0;
					
					movimientos1 = 0;
					movimientos2 = 0;
					
					pos1 = 1;
					pos2 = 1;
					
					jug1 = false;
					jug2 = true;
					
					marcar1 = false;
					marcar2 = false;
					marcar3 = false;
					marcar4 = false;
					marcar5 = false;
					marcar6 = false;
					marcar7 = false;
					marcar8 = false;
					marcar9 = false;
					
					Marcar1 = false;
					Marcar2 = false;
					Marcar3 = false;
					Marcar4 = false;
					Marcar5 = false;
					Marcar6 = false;
					Marcar7 = false;
					Marcar8 = false;
					Marcar9 = false;
					
					Tiempo(50);
					
					Color("negro", "amarillo");
					columna = 69; //lado iskierdo
					for( fila = 8; fila<=21; fila=fila+1 ){
						Posicion(columna, fila);Console.WriteLine(" ");
					}
					
					Tiempo(50);
					
					columna = 81; //lado iskierdo
					for( fila = 8; fila<=21; fila=fila+1 ){
						Posicion(columna, fila);Console.WriteLine(" ");
					}
					
					Tiempo(50);
					
					fila = 12;
					for( columna = 60; columna<=90; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("▄");
					}
					
					Tiempo(50);
					
					fila = 17;
					for( columna = 60; columna<=90; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("▄");
					}
					
					Tiempo(500);
					
					if(pantalla_empates == true)
					{
						Color("blanco", "negro");
						for (fila = 2; fila <=27; fila++) {
							for (columna = 36; columna <= 115; columna++) {
								Posicion(columna, fila);
								Console.WriteLine(" ");
							}
						}
						
						if(idioma == "español")
						{
							Color("negro", "amarillo");
							Posicion(36,22);
							Console.WriteLine("                                                                                ");
							Posicion(36,23);
							Console.WriteLine("                                    EMPATE                                      ");
							Posicion(36,24);
							Console.WriteLine("                                                                                ");
							Color("negro", "amarillo");
							Posicion(55,5);
							Console.WriteLine("Preciona cualquier tecla para continuar ");
						}
						if(idioma == "english")
						{
							Color("negro", "amarillo");
							Posicion(36,22);
							Console.WriteLine("                                                                                ");
							Posicion(36,23);
							Console.WriteLine("                                     DRAW                                       ");
							Posicion(36,24);
							Console.WriteLine("                                                                                ");
							Color("negro", "amarillo");
							Posicion(55,5);
							Console.WriteLine("       Press any key to continue        ");
						}
						
						string[] arteASCII3 =
						{
							"                      █████████         ",
							"  ███████          ███▒▒▒▒▒▒▒▒███       ",
							"  █▒▒▒▒▒▒█       ███▒▒▒▒▒▒▒▒▒▒▒▒▒███    ",
							"   █▒▒▒▒▒▒█    ██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██   ",
							"    █▒▒▒▒▒█   ██▒▒▒▒▒██▒▒▒▒▒▒██▒▒▒▒▒██  ",
							"     █▒▒▒▒█  █▒▒▒▒▒▒████▒▒▒▒████▒▒▒▒▒██ ",
							"      █▒▒▒█  █▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒█ ",
							"    █████████████▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒█ ",
							"   █▒▒▒▒▒▒▒▒▒▒▒▒█▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒█ ",
							" ██▒▒▒▒▒▒▒▒▒▒▒▒▒█▒▒██▒▒▒▒▒▒▒▒▒▒██▒▒▒▒▒█ ",
							"██▒▒▒███████████▒▒▒▒██▒▒▒▒▒▒▒▒██▒▒▒▒▒▒█ ",
							"█▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒█▒▒▒▒▒████████▒▒▒▒▒▒▒██ ",
							"██▒▒▒▒▒▒▒▒▒▒▒▒▒▒█▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██  ",
							" █▒▒▒███████████▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██    ",
							" ██▒▒▒▒▒▒▒▒▒▒████▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒█      ",
							"  ████████████░░░█████████████████      "
						};

						// Imprimir el arte ASCII con Posicion()
						for (int i = 0; i < arteASCII3.Length; i++)
						{
							Color("negro", "amarillo");
							Posicion(55, 6 + i);
							Console.WriteLine(arteASCII3[i]);
						}
						
						Precionar();
						
						Color("blanco", "negro");
						for (fila = 2; fila <=27; fila++) {
							for (columna = 36; columna <= 115; columna++) {
								Posicion(columna, fila);
								Console.WriteLine(" ");
							}
						}
					}
				}
				
				if (tecla.KeyChar == '+')
				{
					bool salir_ajustes = false;
					
					Color("blanco", "negro");
					for (fila = 2; fila <=27; fila++) {
						for (columna = 36; columna <= 115; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
					
					do
					{
						Color("blanco", "negro");
						for (fila = 4; fila <=26; fila++) {
							for (columna = 80; columna <= 115; columna++) {
								Posicion(columna, fila);
								Console.WriteLine(" ");
							}
						}
						
						if(idioma == "español")
						{
							Color("negro", "blanco");
							Posicion(36,2);
							Console.WriteLine("                           CONFIGURACION DEL JUEGO                              ");
							
							Color("amarillo", "negro");
							Posicion(82,26);
							Console.Write("configuracion predeterminada ");
							
							Color("negro", "blanco");
							Console.Write(" P ");
						}
						if(idioma == "english")
						{
							Color("negro", "blanco");
							Posicion(36,2);
							Console.WriteLine("                             GAME CONFIGURATION                                 ");

							Color("amarillo", "negro");
							Posicion(89,26);
							Console.Write("default configuration ");

							Color("negro", "blanco");
							Console.Write(" D ");
						}
						
						
						if(mostrar_movimientos == true)
						{
							Color("blanco", "verde");
						}
						if(mostrar_movimientos == false)
						{
							Color("blanco", "rojo");
						}
						Posicion(47,4);
						Console.WriteLine("                         ");
						Posicion(47,5);
						Console.WriteLine("                         ");
						Posicion(47,6);
						Console.WriteLine("                         ");
						
						if(idioma == "español")
						{
							Color("blanco", "negro");
							Posicion(48,4);
							Console.WriteLine("                       ");
							Posicion(48,5);
							Console.WriteLine("  Mostrar movimientos  ");
							Posicion(48,6);
							Console.WriteLine("                       ");
						}
						if(idioma == "english")
						{
							Color("blanco", "negro");
							Posicion(48,4);
							Console.WriteLine("                       ");
							Posicion(48,5);
							Console.WriteLine("    Show movements     ");
							Posicion(48,6);
							Console.WriteLine("                       ");
						}
						
						if(mostrar_victorias == true)
						{
							Color("blanco", "verde");
						}
						if(mostrar_victorias == false)
						{
							Color("blanco", "rojo");
						}
						Posicion(47,8);
						Console.WriteLine("                         ");
						Posicion(47,9);
						Console.WriteLine("                         ");
						Posicion(47,10);
						Console.WriteLine("                         ");
						
						if(idioma == "español")
						{
							Color("blanco", "negro");
							Posicion(48,8);
							Console.WriteLine("                       ");
							Posicion(48,9);
							Console.WriteLine("   Mostrar victorias   ");
							Posicion(48,10);
							Console.WriteLine("                       ");
						}
						if(idioma == "english")
						{
							Color("blanco", "negro");
							Posicion(48,8);
							Console.WriteLine("                       ");
							Posicion(48,9);
							Console.WriteLine("      Show Wins        ");
							Posicion(48,10);
							Console.WriteLine("                       ");
						}

						
						if(mostrar_rondas == true)
						{
							Color("blanco", "verde");
						}
						if(mostrar_rondas == false)
						{
							Color("blanco", "rojo");
						}
						Posicion(47,12);
						Console.WriteLine("                         ");
						Posicion(47,13);
						Console.WriteLine("                         ");
						Posicion(47,14);
						Console.WriteLine("                         ");
						
						if(idioma == "español")
						{
							Color("blanco", "negro");
							Posicion(48,12);
							Console.WriteLine("                       ");
							Posicion(48,13);
							Console.WriteLine("    Mostrar rondas     ");
							Posicion(48,14);
							Console.WriteLine("                       ");
						}
						if(idioma == "english")
						{
							Color("blanco", "negro");
							Posicion(48,12);
							Console.WriteLine("                       ");
							Posicion(48,13);
							Console.WriteLine("      Show Rounds      ");
							Posicion(48,14);
							Console.WriteLine("                       ");
						}

						
						if(pantalla_victorias == true)
						{
							Color("blanco", "verde");
						}
						if(pantalla_victorias == false)
						{
							Color("blanco", "rojo");
						}
						Posicion(47,16);
						Console.WriteLine("                         ");
						Posicion(47,17);
						Console.WriteLine("                         ");
						Posicion(47,18);
						Console.WriteLine("                         ");
						
						if(idioma == "español")
						{
							Color("blanco", "negro");
							Posicion(48,16);
							Console.WriteLine("                       ");
							Posicion(48,17);
							Console.WriteLine(" Pantalla de victorias ");
							Posicion(48,18);
							Console.WriteLine("                       ");
						}
						if(idioma == "english")
						{
							Color("blanco", "negro");
							Posicion(48,16);
							Console.WriteLine("                       ");
							Posicion(48,17);
							Console.WriteLine("      Win Screen       ");
							Posicion(48,18);
							Console.WriteLine("                       ");
						}

						
						if(pantalla_empates == true)
						{
							Color("blanco", "verde");
						}
						if(pantalla_empates == false)
						{
							Color("blanco", "rojo");
						}
						Posicion(47,20);
						Console.WriteLine("                         ");
						Posicion(47,21);
						Console.WriteLine("                         ");
						Posicion(47,22);
						Console.WriteLine("                         ");
						
						if(idioma == "español")
						{
							Color("blanco", "negro");
							Posicion(48,20);
							Console.WriteLine("                       ");
							Posicion(48,21);
							Console.WriteLine("  Pantalla de empate   ");
							Posicion(48,22);
							Console.WriteLine("                       ");
						}
						if(idioma == "english")
						{
							Color("blanco", "negro");
							Posicion(48,20);
							Console.WriteLine("                       ");
							Posicion(48,21);
							Console.WriteLine("      Tie Screen       ");
							Posicion(48,22);
							Console.WriteLine("                       ");
						}

						
						if(mostrar_pos == true)
						{
							Color("blanco", "verde");
						}
						if(mostrar_pos == false)
						{
							Color("blanco", "rojo");
						}
						Posicion(47,24);
						Console.WriteLine("                         ");
						Posicion(47,25);
						Console.WriteLine("                         ");
						Posicion(47,26);
						Console.WriteLine("                         ");
						
						if(idioma == "español")
						{
							Color("blanco", "negro");
							Posicion(48,24);
							Console.WriteLine("                       ");
							Posicion(48,25);
							Console.WriteLine("  Posicion de casilla  ");
							Posicion(48,26);
							Console.WriteLine("                       ");
						}
						if(idioma == "english")
						{
							Color("blanco", "negro");
							Posicion(48,24);
							Console.WriteLine("                       ");
							Posicion(48,25);
							Console.WriteLine("     Cell Position     ");
							Posicion(48,26);
							Console.WriteLine("                       ");
						}

						
						if(opcion == 1)
						{
							if(idioma == "español")
							{
								Color("negro", "blanco");
								Posicion(48,4);
								Console.WriteLine("                       ");
								Posicion(48,5);
								Console.WriteLine("  Mostrar movimientos  ");
								Posicion(48,6);
								Console.WriteLine("                       ");
								
								Color("blanco", "negro");
								Posicion(84,8);
								Console.WriteLine("Muestra exactamente");
								Posicion(84,9);
								Console.WriteLine("los movimientos que");
								Posicion(84,10);
								Console.WriteLine("hiceron cada uno de");
								Posicion(84,11);
								Console.WriteLine("los jugadores.");
								
								Posicion(84,13);
								Console.WriteLine("Este numuero se vera");
								Posicion(84,14);
								Console.WriteLine("en la parte iskierda");
								Posicion(84,15);
								Console.WriteLine("superior.");
							}
							if(idioma == "english")
							{
								Color("negro", "blanco");
								Posicion(48,4);
								Console.WriteLine("                       ");
								Posicion(48,5);
								Console.WriteLine("    Show movements     ");
								Posicion(48,6);
								Console.WriteLine("                       ");
								
								Color("blanco", "negro");
								Posicion(84,8);
								Console.WriteLine("Shows exactly");
								Posicion(84,9);
								Console.WriteLine("the moves made");
								Posicion(84,10);
								Console.WriteLine("by each of");
								Posicion(84,11);
								Console.WriteLine("the players.");
								
								Posicion(84,13);
								Console.WriteLine("This number will be");
								Posicion(84,14);
								Console.WriteLine("seen on the top");
								Posicion(84,15);
								Console.WriteLine("left corner.");
							}

						}
						if(opcion == 2)
						{
							if(idioma == "español")
							{
								Color("negro", "blanco");
								Posicion(48,8);
								Console.WriteLine("                       ");
								Posicion(48,9);
								Console.WriteLine("   Mostrar victorias   ");
								Posicion(48,10);
								Console.WriteLine("                       ");
								
								Color("blanco", "negro");
								Posicion(84,8);
								Console.WriteLine("Muestra exactamente");
								Posicion(84,9);
								Console.WriteLine("las victorias que");
								Posicion(84,10);
								Console.WriteLine("han hecho cada uno");
								Posicion(84,11);
								Console.WriteLine("de los jugadores.");
								
								Posicion(84,13);
								Console.WriteLine("Este numuero se vera");
								Posicion(84,14);
								Console.WriteLine("en la parte iskierda");
								Posicion(84,15);
								Console.WriteLine("inferior.");
							}
							if(idioma == "english")
							{
								Color("negro", "blanco");
								Posicion(48,8);
								Console.WriteLine("                       ");
								Posicion(48,9);
								Console.WriteLine("      Show wins        ");
								Posicion(48,10);
								Console.WriteLine("                       ");
								
								Color("blanco", "negro");
								Posicion(84,8);
								Console.WriteLine("Shows exactly");
								Posicion(84,9);
								Console.WriteLine("the victories that");
								Posicion(84,10);
								Console.WriteLine("each of the players");
								Posicion(84,11);
								Console.WriteLine("has achieved.");
								
								Posicion(84,13);
								Console.WriteLine("This number will be");
								Posicion(84,14);
								Console.WriteLine("seen on the bottom");
								Posicion(84,15);
								Console.WriteLine("left corner.");
							}

						}
						if(opcion == 3)
						{
							if(idioma == "español")
							{
								Color("negro", "blanco");
								Posicion(48,12);
								Console.WriteLine("                       ");
								Posicion(48,13);
								Console.WriteLine("    Mostrar rondas     ");
								Posicion(48,14);
								Console.WriteLine("                       ");
								
								Color("blanco", "negro");
								Posicion(84,8);
								Console.WriteLine("Muestra las rondas");
								Posicion(84,9);
								Console.WriteLine("en tiempo real que");
								Posicion(84,10);
								Console.WriteLine("se van llevando a");
								Posicion(84,11);
								Console.WriteLine("cabo en el trascurso");
								Posicion(84,12);
								Console.WriteLine("de la partida.");
								
								Posicion(84,14);
								Console.WriteLine("el numero de rondas");
								Posicion(84,15);
								Console.WriteLine("se mostrara en la");
								Posicion(84,16);
								Console.WriteLine("parte superior en");
								Posicion(84,17);
								Console.WriteLine("el centro.");
							}
							if(idioma == "english")
							{
								Color("negro", "blanco");
								Posicion(48,12);
								Console.WriteLine("                       ");
								Posicion(48,13);
								Console.WriteLine("      Show rounds      ");
								Posicion(48,14);
								Console.WriteLine("                       ");
								
								Color("blanco", "negro");
								Posicion(84,8);
								Console.WriteLine("Shows the rounds");
								Posicion(84,9);
								Console.WriteLine("in real time that");
								Posicion(84,10);
								Console.WriteLine("are taking place");
								Posicion(84,11);
								Console.WriteLine("during the match.");
								
								Posicion(84,14);
								Console.WriteLine("the number of rounds");
								Posicion(84,15);
								Console.WriteLine("will be shown in the");
								Posicion(84,16);
								Console.WriteLine("upper part at");
								Posicion(84,17);
								Console.WriteLine("the center.");
							}

						}
						if(opcion == 4)
						{
							if(idioma == "español")
							{
								Color("negro", "blanco");
								Posicion(48,16);
								Console.WriteLine("                       ");
								Posicion(48,17);
								Console.WriteLine(" Pantalla de victorias ");
								Posicion(48,18);
								Console.WriteLine("                       ");
								
								Color("blanco", "negro");
								Posicion(84,8);
								Console.WriteLine("muestra una pantalla");
								Posicion(84,9);
								Console.WriteLine("especificando cual de");
								Posicion(84,10);
								Console.WriteLine("los jugadores gano la");
								Posicion(84,11);
								Console.WriteLine("ronda, esperando que");
								Posicion(84,12);
								Console.WriteLine("cualquier juagador");
								Posicion(84,13);
								Console.WriteLine("precione cualquier");
								Posicion(84,14);
								Console.WriteLine("tecla para continuar");
								Posicion(84,15);
								Console.WriteLine("a la siguiente ronda.");
							}
							if(idioma == "english")
							{
								Color("negro", "blanco");
								Posicion(48,16);
								Console.WriteLine("                       ");
								Posicion(48,17);
								Console.WriteLine("      Win screen       ");
								Posicion(48,18);
								Console.WriteLine("                       ");
								
								Color("blanco", "negro");
								Posicion(84,8);
								Console.WriteLine("displays a screen");
								Posicion(84,9);
								Console.WriteLine("specifying which of");
								Posicion(84,10);
								Console.WriteLine("the players won the");
								Posicion(84,11);
								Console.WriteLine("round, waiting for");
								Posicion(84,12);
								Console.WriteLine("any player to press");
								Posicion(84,13);
								Console.WriteLine("any key to proceed");
								Posicion(84,14);
								Console.WriteLine("to the next round.");
							}

						}
						if(opcion == 5)
						{
							if(idioma == "español")
							{
								Color("negro", "blanco");
								Posicion(48,20);
								Console.WriteLine("                       ");
								Posicion(48,21);
								Console.WriteLine("  Pantalla de empate   ");
								Posicion(48,22);
								Console.WriteLine("                       ");
								
								Color("blanco", "negro");
								Posicion(84,8);
								Console.WriteLine("muestra una pantalla");
								Posicion(84,9);
								Console.WriteLine("especificando que nadie");
								Posicion(84,10);
								Console.WriteLine("de los jugadores gano");
								Posicion(84,11);
								Console.WriteLine("esperando que cualquiera");
								Posicion(84,12);
								Console.WriteLine("de los juagadores");
								Posicion(84,13);
								Console.WriteLine("precione cualquier");
								Posicion(84,14);
								Console.WriteLine("tecla para continuar");
								Posicion(84,15);
								Console.WriteLine("a la siguiente ronda.");
							}
							if(idioma == "english")
							{
								Color("negro", "blanco");
								Posicion(48,20);
								Console.WriteLine("                       ");
								Posicion(48,21);
								Console.WriteLine("      Tie screen       ");
								Posicion(48,22);
								Console.WriteLine("                       ");
								
								Color("blanco", "negro");
								Posicion(84,8);
								Console.WriteLine("displays a screen");
								Posicion(84,9);
								Console.WriteLine("specifying that none");
								Posicion(84,10);
								Console.WriteLine("of the players won,");
								Posicion(84,11);
								Console.WriteLine("waiting for any of");
								Posicion(84,12);
								Console.WriteLine("the players to press");
								Posicion(84,13);
								Console.WriteLine("any key to proceed");
								Posicion(84,14);
								Console.WriteLine("to the next round.");
							}

						}
						if(opcion == 6)
						{
							if(idioma == "español")
							{
								Color("negro", "blanco");
								Posicion(48,24);
								Console.WriteLine("                       ");
								Posicion(48,25);
								Console.WriteLine("  Posicion de casilla  ");
								Posicion(48,26);
								Console.WriteLine("                       ");
								
								Color("blanco", "negro");
								Posicion(84,8);
								Console.WriteLine("Muestra la posicion");
								Posicion(84,9);
								Console.WriteLine("en tiempo real de la");
								Posicion(84,10);
								Console.WriteLine("casilla en la que el");
								Posicion(84,11);
								Console.WriteLine("jugador esta parado");
								Posicion(84,12);
								Console.WriteLine("opcion posicionado");
								Posicion(84,13);
								Console.WriteLine("en ese momento.");
								
								Posicion(84,15);
								Console.WriteLine("este numero estara");
								Posicion(84,16);
								Console.WriteLine("en la parte superior");
								Posicion(84,17);
								Console.WriteLine("derecha.");
							}
							if(idioma == "english")
							{
								Color("negro", "blanco");
								Posicion(48,24);
								Console.WriteLine("                       ");
								Posicion(48,25);
								Console.WriteLine("    Cell position      ");
								Posicion(48,26);
								Console.WriteLine("                       ");
								
								Color("blanco", "negro");
								Posicion(84,8);
								Console.WriteLine("Displays the real-time");
								Posicion(84,9);
								Console.WriteLine("position of the cell");
								Posicion(84,10);
								Console.WriteLine("where the player is");
								Posicion(84,11);
								Console.WriteLine("standing at that moment.");
								
								Posicion(84,15);
								Console.WriteLine("this number will be");
								Posicion(84,16);
								Console.WriteLine("in the upper right");
								Posicion(84,17);
								Console.WriteLine("corner.");
							}

						}
						
						
						Precionar();
						
						switch(tecla.Key)
						{
							case ConsoleKey.Escape:
								
								salir_ajustes = true;
								break;
								
							case ConsoleKey.UpArrow:
								
								opcion = Math.Max(opcion - 1, 1);
								break;
								
							case ConsoleKey.DownArrow:
								
								opcion = Math.Min(opcion + 1, 6);
								break;
								
							case ConsoleKey.Enter:
							case ConsoleKey.Spacebar:
								
								if(opcion == 1)
								{
									if(mostrar_movimientos == false)
									{
										mostrar_movimientos = true;
										break;
									}
									if(mostrar_movimientos == true)
									{
										mostrar_movimientos = false;
										break;
									}
								}
								if(opcion == 2)
								{
									if(mostrar_victorias == true)
									{
										mostrar_victorias = false;
										break;
									}
									if(mostrar_victorias == false)
									{
										mostrar_victorias = true;
										break;
									}
								}
								if(opcion == 3)
								{
									if(mostrar_rondas == false)
									{
										mostrar_rondas = true;
										break;
									}
									if(mostrar_rondas == true)
									{
										mostrar_rondas = false;
										break;
									}
								}
								if(opcion == 4)
								{
									if(pantalla_victorias == true)
									{
										pantalla_victorias = false;
										break;
									}
									if(pantalla_victorias == false)
									{
										pantalla_victorias = true;
										break;
									}
								}
								if(opcion == 5)
								{
									if(pantalla_empates == false)
									{
										pantalla_empates = true;
										break;
									}
									if(pantalla_empates == true)
									{
										pantalla_empates = false;
										break;
									}
								}
								if(opcion == 6)
								{
									if(mostrar_pos == false)
									{
										mostrar_pos = true;
										break;
									}
									if(mostrar_pos == true)
									{
										mostrar_pos = false;
										break;
									}
								}
								break;
								
							case ConsoleKey.P:
							case ConsoleKey.D:
								
								mostrar_movimientos = false;
								mostrar_rondas = true;
								mostrar_pos = false;
								mostrar_victorias = true;
								pantalla_empates = false;
								pantalla_victorias = false;
								break;
						}
						
					}while(salir_ajustes == false);
					
					Color("blanco", "negro");
					for (fila = 2; fila <=27; fila++) {
						for (columna = 36; columna <= 115; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
				}
				
			}while(salir_juego1 == false);
			
			opcion = 4;
			
			Color("blanco", "blanco");
			for (fila = 2; fila <=27; fila++) {
				for (columna = 36; columna <= 115; columna++) {
					Posicion(columna, fila);
					Console.WriteLine(" ");
				}
			}
			
		}

		static void AyudaInicio()
		{
			opcion = 1;
			
			Reset();
			tui();
			
			do{
				
				Color("negro", "blanco");
				for (fila = 2; fila <=27; fila++) {
					for (columna = 67; columna <= 116; columna++) {
						Posicion(columna, fila);
						Console.WriteLine(" ");
					}
				}
				
				Color("negro", "azul");
				for (fila = 2; fila <=27; fila++) {
					for (columna = 4; columna <= 66; columna++) {
						Posicion(columna, fila);
						Console.WriteLine(" ");
					}
				}
				
				Cursor(false);
				
				if(opcion == 1)
				{
					Color("azul", "blanco");
					columna = 22; //lado iskierdo
					for( fila = 8; fila<=9; fila=fila+1 ){
						Posicion(columna, fila);Console.WriteLine(" ");
					}
					fila = 10;
					for( columna = 22; columna<=44; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("▄");
					}
					
					Color("negro", "negro");
					for (fila = 7; fila <=9; fila++) {
						for (columna = 23; columna <= 45; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
					
					if(idioma == "español")
					{
						Color("blanco", "negro");
						Posicion(30,1);
						Console.WriteLine(" ACERCA DE ");

						ColorRandomNegro();
						Posicion(30,8);
						Console.WriteLine(" DIAMOND");
						
						Color("blanco", "azul");
						Posicion(30,12);
						Console.WriteLine("CONTACTOS");
						
						Posicion(28,16);
						Console.WriteLine("USO DE DIAMOND");
						
						Posicion(30,20);
						Console.WriteLine("  AYUDA");
						
						
						Color("azul", "blanco");
						Posicion(76,5);
						Console.Write("DIAMOND ");
						Color("negro", "blanco");
						Console.Write("es una empresa dirijida a");
						Posicion(76,6);
						Console.Write("los estudiantes del pascual bravo");
						Posicion(76,7);
						Console.Write("proporcionando un programa por el");
						Posicion(76,8);
						Console.Write("momento basico, diamond sera util");
						Posicion(76,9);
						Console.Write("para mero entretenimiento y hacer");
						Posicion(76,10);
						Console.Write("algunas tareas simples con la IA");
					}
					if(idioma == "english")
					{
						Color("blanco", "negro");
						Posicion(29,1);
						Console.WriteLine("   ABOUT   ");

						ColorRandomNegro();
						Posicion(30,8);
						Console.WriteLine(" DIAMOND");
						
						Color("blanco", "azul");
						Posicion(30,12);
						Console.WriteLine("CONTACTS");
						
						Posicion(28,16);
						Console.WriteLine("USE OF DIAMOND");
						
						Posicion(30,20);
						Console.WriteLine("   HELP");
						
						Color("azul", "blanco");
						Posicion(76,5);
						Console.Write("DIAMOND ");
						Color("negro", "blanco");
						Console.Write("is a company aimed at");
						Posicion(76,6);
						Console.Write("the students of Pascual Bravo");
						Posicion(76,7);
						Console.Write("providing a basic program for now,");
						Posicion(76,8);
						Console.Write("diamond will be useful");
						Posicion(76,9);
						Console.Write("for mere entertainment and to do");
						Posicion(76,10);
						Console.Write("some simple tasks with AI");
					}
					
					Color("negro", "blanco");
					Posicion(76,26);
					Console.Write("VERSION ");
					ColorRandomBlanco();
					Console.WriteLine("4.9");
					
					char caract1 = '▀';
					char caract2 = '▄';
					char caract3 = '█';
					
					// Dibujo de la estructura
					Color("azul", "blanco");

					fila = 17;
					for (columna = 88; columna <= 98; columna++)
					{
						Posicion(columna, fila);
						Console.Write(caract1);
					}

					// Línea abajo
					fila = 21;
					for (columna = 88; columna <= 98; columna++)
					{
						Posicion(columna, fila);
						Console.Write(caract2);
					}

					// Línea derecha
					columna = 98;
					for (fila = 17; fila <= 21; fila++)
					{
						Posicion(columna, fila);
						Console.Write(caract3);
					}

					// Línea izquierda
					columna = 88;
					for (fila = 17; fila <= 21; fila++)
					{
						Posicion(columna, fila);
						Console.Write(caract3);
					}

					// Cambio de color para la otra parte
					Color("negro", "blanco");

					// Línea arriba
					fila = 15;
					for (columna = 83; columna <= 93; columna++)
					{
						Posicion(columna, fila);
						Console.Write(caract1);
					}

					// Línea abajo
					fila = 19;
					for (columna = 83; columna <= 93; columna++)
					{
						Color("azul", "blanco");
						Posicion(88, 19);
						Console.Write(caract3);
						Color("negro", "blanco");
						Posicion(columna, fila);
						Console.Write(caract2);
					}

					// Línea derecha
					columna = 93;
					for (fila = 15; fila <= 19; fila++)
					{
						Posicion(columna, fila);
						Console.Write(caract3);
					}

					// Línea izquierda
					columna = 83;
					for (fila = 15; fila <= 19; fila++)
					{
						Posicion(columna, fila);
						Console.Write(caract3);
					}
				}
				
				if(opcion == 2)
				{
					Color("azul", "blanco");
					columna = 22; //lado iskierdo
					for( fila = 12; fila<=13; fila=fila+1 ){
						Posicion(columna, fila);Console.WriteLine(" ");
					}
					fila = 14;
					for( columna = 22; columna<=44; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("▄");
					}
					
					Color("negro", "negro");
					for (fila = 11; fila <=13; fila++) {
						for (columna = 23; columna <= 45; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
					
					Color("negro", "gris");
					for (fila = 2; fila <=14; fila++) {
						for (columna = 68; columna <= 115; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
					
					Color("negro", "grisoscuro");
					for (fila = 15; fila <=27; fila++) {
						for (columna = 68; columna <= 115; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
					
					if(idioma == "español")
					{
						ColorRandomNegro();
						Posicion(30,12);
						Console.WriteLine("CONTACTOS");
						
						Color("blanco", "azul");
						Posicion(30,8);
						Console.WriteLine(" DIAMOND");
						
						Posicion(28,16);
						Console.WriteLine("USO DE DIAMOND");
						
						Posicion(30,20);
						Console.WriteLine("  AYUDA");
						
						
						Color("blanco", "grisoscuro");
						Posicion(75,2);
						Console.WriteLine(" INSTITUCION UNIVERSITARIA PASCUAL BRAVO ");
						
						Color("negro", "gris");
						Posicion(72,6);
						Console.WriteLine("CELULAR: 3244534596");
						
						Posicion(72,8);
						Console.WriteLine("CORREO: santiago.barrera447@pascualbravo...");
						
						
						Color("blanco", "negro");
						Posicion(74,12);
						Console.Write("                                    ");
						Posicion(73,13);
						Console.Write("       SANTIAGO BARRERA ARIAS         ");
						Posicion(72,14);
						Console.Write("                                        ");
						
						Color("negro", "blanco");
						Posicion(72,15);
						Console.Write("                                        ");
						Posicion(73,16);
						Console.Write("      JOSE MANUEL GIRALDO RIOS        ");
						Posicion(74,17);
						Console.Write("                                    ");
						
						Color("blanco", "grisoscuro");
						Posicion(72,21);
						Console.WriteLine("CELULAR: 3005383677");
						
						Posicion(72,23);
						Console.WriteLine("CORREO: jose.giraldo635@pascualbravo...");
						
						Color("negro", "gris");
						Posicion(98,27);
						Console.WriteLine(" Prof. Jaime Meza ");
					}
					if(idioma == "english")
					{
						ColorRandomNegro();
						Posicion(30,12);
						Console.WriteLine("CONTACTS");

						Color("blanco", "azul");
						Posicion(30,8);
						Console.WriteLine(" DIAMOND");

						Posicion(28,16);
						Console.WriteLine("USE OF DIAMOND");

						Posicion(30,20);
						Console.WriteLine("  HELP");


						Color("blanco", "grisoscuro");
						Posicion(76,2);
						Console.WriteLine(" UNIVERSITARY INSTITUTION PASCUAL BRAVO ");
						
						Color("negro", "gris");
						Posicion(72,6);
						Console.WriteLine("PHONE: 3245457324");

						Posicion(72,8);
						Console.WriteLine("EMAIL: santiago.barrera447@pascualbravo...");

						Color("blanco", "negro");
						Posicion(74,12);
						Console.Write("                                    ");
						Posicion(73,13);
						Console.Write("       SANTIAGO BARRERA ARIAS         ");
						Posicion(72,14);
						Console.Write("                                        ");

						Color("negro", "blanco");
						Posicion(72,15);
						Console.Write("                                        ");
						Posicion(73,16);
						Console.Write("      JOSE MANUEL GIRALDO RIOS        ");
						Posicion(74,17);
						Console.Write("                                    ");

						Color("blanco", "grisoscuro");
						Posicion(72,21);
						Console.WriteLine("PHONE: 3236754678");

						Posicion(72,23);
						Console.WriteLine("EMAIL: jose.giraldo635@pascualbravo...");
						
						Color("negro", "gris");
						Posicion(97,27);
						Console.WriteLine(" Teach. Jaime Meza ");
					}
				}
				
				if(opcion == 3)
				{
					Color("azul", "blanco");
					columna = 22; //lado iskierdo
					for( fila = 16; fila<=17; fila=fila+1 ){
						Posicion(columna, fila);Console.WriteLine(" ");
					}
					fila = 18;
					for( columna = 22; columna<=44; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("▄");
					}
					
					Color("negro", "negro");
					for (fila = 15; fila <=17; fila++) {
						for (columna = 23; columna <= 45; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
					
					Color("negro", "grisoscuro");
					for (fila = 4; fila <=15; fila++) {
						for (columna = 70; columna <= 113; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
					Color("negro", "gris");
					for (fila = 16; fila <=26; fila++) {
						for (columna = 70; columna <= 113; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
					
					if(idioma == "español")
					{
						ColorRandomNegro();
						Posicion(28,16);
						Console.WriteLine("USO DE DIAMOND");
						
						Color("blanco", "azul");
						Posicion(30,8);
						Console.WriteLine(" DIAMOND");
						
						Posicion(30,12);
						Console.WriteLine("CONTACTOS");
						
						Posicion(30,20);
						Console.WriteLine("  AYUDA");
						
						Color("blanco", "grisoscuro");
						Posicion(76,5);
						Console.Write("Aunque Diamond esta hecho en una");
						Posicion(76,6);
						Console.Write("aplicacion de consola, su uso es");
						Posicion(76,7);
						Console.Write("mas sencillo ya que ademas de la");
						Posicion(76,8);
						Console.Write("entrada de texto se utilizan las");
						Posicion(76,9);
						Console.Write("teclas, haciendolo mas dinamico.");
						
						Posicion(76,11);
						Console.Write("Estas son las teclas que se usan");
						Posicion(76,12);
						Console.Write("dentro del programa para moverte");
						Posicion(76,13);
						Console.Write("o realizar acciones diferentes a");
						Posicion(76,14);
						Console.Write("ingresar texto.");
					}
					if(idioma == "english")
					{
						ColorRandomNegro();
						Posicion(28,16);
						Console.WriteLine("USE OF DIAMOND");

						Color("blanco", "azul");
						Posicion(30,8);
						Console.WriteLine(" DIAMOND");

						Posicion(30,12);
						Console.WriteLine("CONTACTS");

						Posicion(30,20);
						Console.WriteLine("  HELP");

						Color("blanco", "grisoscuro");
						Posicion(76,5);
						Console.Write("Although Diamond is made in a");
						Posicion(76,6);
						Console.Write("console application, its usage is");
						Posicion(76,7);
						Console.Write("simpler since, in addition to");
						Posicion(76,8);
						Console.Write("text input, it uses keys,");
						Posicion(76,9);
						Console.Write("making it more dynamic.");

						Posicion(76,11);
						Console.Write("These are the keys used");
						Posicion(76,12);
						Console.Write("within the program to move");
						Posicion(76,13);
						Console.Write("or perform different actions");
						Posicion(76,14);
						Console.Write("than entering text.");
					}
					
					Color("negro", "gris");
					Posicion(83,23);
					Console.WriteLine("┌───┐");
					Posicion(83,24);
					Console.WriteLine("│ ← │");
					Posicion(83,25);
					Console.WriteLine("└───┘");
					
					Posicion(95,23);
					Console.WriteLine("┌───┐");
					Posicion(95,24);
					Console.WriteLine("│ → │");
					Posicion(95,25);
					Console.WriteLine("└───┘");
					
					Posicion(89,20);
					Console.WriteLine("┌───┐");
					Posicion(89,21);
					Console.WriteLine("│ ↑ │");
					Posicion(89,22);
					Console.WriteLine("└───┘");
					
					Posicion(89,23);
					Console.WriteLine("┌───┐");
					Posicion(89,24);
					Console.WriteLine("│ ↓ │");
					Posicion(89,25);
					Console.WriteLine("└───┘");
					
					Posicion(79,20);
					Console.WriteLine("┌─────┐");
					Posicion(79,21);
					Console.WriteLine("│ Esc │");
					Posicion(79,22);
					Console.WriteLine("└─────┘");
					
					Posicion(97,20);
					Console.WriteLine("┌───────┐");
					Posicion(97,21);
					Console.WriteLine("│ Intro │");
					Posicion(97,22);
					Console.WriteLine("└───────┘");
					
					
					Posicion(85,17);
					Console.WriteLine("┌───┐");
					Posicion(85,18);
					Console.WriteLine("│ + │");
					Posicion(85,19);
					Console.WriteLine("└───┘");
					
					Posicion(93,17);
					Console.WriteLine("┌───┐");
					Posicion(93,18);
					Console.WriteLine("│ - │");
					Posicion(93,19);
					Console.WriteLine("└───┘");
				}
				
				if(opcion == 4)
				{
					Color("azul", "blanco");
					columna = 22; //lado iskierdo
					for( fila = 20; fila<=21; fila=fila+1 ){
						Posicion(columna, fila);Console.WriteLine(" ");
					}
					fila = 22;
					for( columna = 22; columna<=44; columna=columna+1 ){
						Posicion(columna, fila);Console.WriteLine("▄");
					}
					
					Color("negro", "negro");
					for (fila = 19; fila <=21; fila++) {
						for (columna = 23; columna <= 45; columna++) {
							Posicion(columna, fila);
							Console.WriteLine(" ");
						}
					}
					
					if(idioma == "español")
					{
						ColorRandomNegro();
						Posicion(30,20);
						Console.WriteLine("  AYUDA");
						
						Color("blanco", "azul");
						Posicion(30,8);
						Console.WriteLine(" DIAMOND");
						
						Posicion(30,12);
						Console.WriteLine("CONTACTOS");
						
						Posicion(28,16);
						Console.WriteLine("USO DE DIAMOND");
						
						
						Color("blanco", "negro");
						Posicion(82,2);
						Console.WriteLine(" PREGUNTAS FRECUENTES ");
						
						Color("negro", "blanco");
						Posicion(69,5);
						Console.Write("- ¿por que cuando pongo mi usuario");
						Posicion(69,6);
						Console.Write("  me sale este aviso amarillo? \"!\"");
						
						ColorRandomBlanco();
						Posicion(69,8);
						Console.Write("♦ Eso se debe a que el usuario que ingresaste");
						Posicion(69,9);
						Console.Write("  no existe en la base de datos, asegurate de");
						Posicion(69,10);
						Console.Write("  escribirlo correctamente o bien crearte una");
						Posicion(69,11);
						Console.Write("  cuenta si no lo habias hecho.");
						
						Color("negro", "blanco");
						Posicion(69,14);
						Console.Write("- ¿por que al crear mi cuenta no me");
						Posicion(69,15);
						Console.Write("  deja poner el usuario que yo quiero?");
						
						ColorRandomBlanco();
						Posicion(69,17);
						Console.Write("♦ El nombre de usuario que intentas poner ya");
						Posicion(69,18);
						Console.Write("  esta siendo utilizado por otra persona.");
					}
					if(idioma == "english")
					{
						ColorRandomNegro();
						Posicion(30,20);
						Console.WriteLine("  HELP");

						Color("blanco", "azul");
						Posicion(30,8);
						Console.WriteLine(" DIAMOND");

						Posicion(30,12);
						Console.WriteLine("CONTACTS");

						Posicion(28,16);
						Console.WriteLine("USE OF DIAMOND");


						Color("blanco", "negro");
						Posicion(82,2);
						Console.WriteLine(" FREQUENTLY ASKED QUESTIONS ");

						Color("negro", "blanco");
						Posicion(69,5);
						Console.Write("- Why do I get this yellow warning when");
						Posicion(69,6);
						Console.Write("  I enter my username? \"!\"");

						ColorRandomBlanco();
						Posicion(69,8);
						Console.Write("♦ This is because the username you entered");
						Posicion(69,9);
						Console.Write("  does not exist in the database, make sure to");
						Posicion(69,10);
						Console.Write("  type it correctly or create an account if you");
						Posicion(69,11);
						Console.Write("  hadn't done so before.");

						Color("negro", "blanco");
						Posicion(69,14);
						Console.Write("- Why can't I use the username I want when");
						Posicion(69,15);
						Console.Write("  creating my account?");

						ColorRandomBlanco();
						Posicion(69,17);
						Console.Write("♦ The username you're trying to use is already");
						Posicion(69,18);
						Console.Write("  being used by someone else.");
					}
				}
				
				
				Color("blanco", "negro");
				Posicion(67,2);
				Console.WriteLine(" < ");
				
				
				
				Precionar();
				
				switch(tecla.Key)
				{
					case ConsoleKey.Escape:
						
						salir = true;
						tecla = new ConsoleKeyInfo('a', ConsoleKey.A, false, false, false);
						break;
						
					case ConsoleKey.UpArrow:
						
						opcion = Math.Max(opcion - 1, 1);
						break;
						
					case ConsoleKey.DownArrow:
						
						opcion = Math.Min(opcion + 1, 4);
						break;
				}

			}while(salir == false);
			
			salir = false;
			opcion = 1;
			
			Color("negro", "negro");
			Reset();
			
			Color("negro", "blanco");
			for (fila = 3; fila <=19; fila++) {
				for (columna = 36; columna <= 84; columna++) {
					Posicion(columna, fila);
					Console.WriteLine(" ");
				}
			}
			
			if(idioma == "español")
			{
				Posicion(51,11);
				Color("negro", "blanco");
				Console.WriteLine(usuario);
				
				if(mostrar == false && contraseña != "")
				{
					Color("gris", "blanco");
					Posicion(54,14);
					Console.WriteLine("oculta");
				}
				else
				{
					Posicion(54,14);
					Console.WriteLine(contraseña);
				}
			}
			if(idioma == "english")
			{
				Posicion(48,11);
				Color("negro", "blanco");
				Console.WriteLine(usuario);
				
				if(mostrar == false && contraseña != "")
				{
					Color("gris", "blanco");
					Posicion(48,14);
					Console.WriteLine("hidden");
				}
				else
				{
					Posicion(48,14);
					Console.WriteLine(contraseña);
				}
			}
			
		}
		
		
		static int[] ObtenerRango(string pregunta)
		{
			// Expresión regular para buscar los números del rango
			Regex regex = new Regex(@"(\d+)");

			// Buscar coincidencias en la pregunta
			MatchCollection matches = regex.Matches(pregunta);

			// Obtener los números encontrados
			int[] numeros = new int[matches.Count];
			for (int i = 0; i < matches.Count; i++)
			{
				if (!int.TryParse(matches[i].Value, out numeros[i]))
				{
					return null;
				}
			}

			// Verificar si se encontraron al menos dos números
			if (numeros.Length >= 2)
			{
				Array.Sort(numeros); // Ordenar los números
				return new int[] { numeros[0], numeros[numeros.Length - 1] }; // Devolver el primer y último número
			}
			else
			{
				return null; // No se encontraron suficientes números
			}
		}
		
		static bool IsSpecialCharacter(char c)
		{
			return !char.IsLetterOrDigit(c) && c != '+' && c != '-' && !char.IsControl(c);
		}
		
		static void CrearCuenta()
		{
			
			Color("negro", "gris");
			Posicion(70,18);
			Console.WriteLine("                ");
			
			if(idioma == "español")
			{
				Posicion(70,19);
				Console.WriteLine("  CREAR CUENTA  ");
			}
			if(idioma == "english")
			{
				Posicion(70,19);
				Console.WriteLine(" CREATE ACCOUNT ");
			}
			Posicion(70,20);
			Console.WriteLine("                ");

			int fila, columna;
			char caract1 = '▀';
			char caract2 = '▄';
			char caract3 = '█';
			
			Color("azul", "blanco");
			fila = 12; // Línea arriba
			for (columna = 75; columna <= 85; columna++)
			{
				Posicion(columna, fila);
				Console.Write("▀");
			}
			
			fila = 16; // Línea abajo
			for (columna = 75; columna <= 85; columna++)
			{
				Posicion(columna, fila);
				Console.Write(caract2);
			}

			columna = 85; // Línea derecha
			for (fila = 12; fila <= 16; fila++)
			{
				Posicion(columna, fila);
				Console.Write(caract3);
			}

			columna = 75; // Línea izquierda
			for (fila = 12; fila <= 16; fila++)
			{
				Posicion(columna, fila);
				Console.Write(caract3);
			}
			
			Color("negro", "blanco");
			
			fila = 10; // Línea arriba
			for (columna = 70; columna <= 80; columna++)
			{
				Posicion(columna, fila);
				Console.Write(caract1);
			}

			fila = 14; // Línea abajo
			for (columna = 70; columna <= 80; columna++)
			{
				Color("azul", "blanco");
				Posicion(75, 14);
				Console.Write(caract3);
				Color("negro", "blanco");
				Posicion(columna, fila);
				Console.Write(caract2);
			}

			columna = 80; // Línea derecha
			for (fila = 10; fila <= 14; fila++)
			{
				Posicion(columna, fila);
				Console.Write(caract3);
			}

			columna = 70; // Línea izquierda
			for (fila = 10; fila <= 14; fila++)
			{
				Posicion(columna, fila);
				Console.Write(caract3);
			}
			
			
			if(opcion == 10)
			{
				
				Color("negro", "blanco");
				fila = 12; // Línea arriba
				for (columna = 75; columna <= 85; columna++)
				{
					Posicion(columna, fila);
					Console.Write("▀");
				}
				
				fila = 16; // Línea abajo
				for (columna = 75; columna <= 85; columna++)
				{
					Posicion(columna, fila);
					Console.Write(caract2);
				}

				columna = 85; // Línea derecha
				for (fila = 12; fila <= 16; fila++)
				{
					Posicion(columna, fila);
					Console.Write(caract3);
				}

				columna = 75; // Línea izquierda
				for (fila = 12; fila <= 16; fila++)
				{
					Posicion(columna, fila);
					Console.Write(caract3);
				}
				
				Color("azul", "blanco");
				
				fila = 10; // Línea arriba
				for (columna = 70; columna <= 80; columna++)
				{
					Posicion(columna, fila);
					Console.Write(caract1);
				}

				fila = 14; // Línea abajo
				for (columna = 70; columna <= 80; columna++)
				{
					Color("negro", "blanco");
					Posicion(75, 14);
					Console.Write(caract3);
					Color("azul", "blanco");
					Posicion(columna, fila);
					Console.Write(caract2);
				}

				columna = 80; // Línea derecha
				for (fila = 10; fila <= 14; fila++)
				{
					Posicion(columna, fila);
					Console.Write(caract3);
				}

				columna = 70; // Línea izquierda
				for (fila = 10; fila <= 14; fila++)
				{
					Posicion(columna, fila);
					Console.Write(caract3);
				}
				
				if(cuentaCreada)
				{
					Color("blanco", "azul");
					Posicion(70,18);
					Console.WriteLine("                ");
					Posicion(70,19);
					Console.WriteLine("                ");
					Posicion(70,20);
					Console.WriteLine("                ");
					
					if(idioma == "español")
					{
						Posicion(70,19);
						Animacion("  CREAR CUENTA ");
					}
					if(idioma == "english")
					{
						Posicion(70,19);
						Animacion(" CREATE ACCOUNT ");
					}
				}
				else
				{
					Color("blanco", "negro");
					Posicion(70,18);
					Console.WriteLine("                ");
					Posicion(70,19);
					Console.WriteLine("                ");
					Posicion(70,20);
					Console.WriteLine("                ");
					
					if(idioma == "español")
					{
						Posicion(70,19);
						Animacion("  FALTAN DATOS ");
					}
					if(idioma == "english")
					{
						Posicion(70,19);
						Animacion("  MISSING DATA ");
					}
				}
				
			}
		}
		
		static void tui()
		{
			
			int fila, columna;
			
			Color("negro", "blanco");
			fila = 1; //linea superior
			for( columna = 3; columna<=116; columna=columna+1 ){
				Posicion(columna, fila);Console.WriteLine("▀");
			}
			
			fila = 28; //linea inferior
			for( columna = 3; columna<=116; columna=columna+1 ){
				Posicion(columna, fila);Console.WriteLine("▄");
			}
			
			columna = 2; //lado iskierdo
			for( fila = 1; fila<=28; fila=fila+1 ){
				Posicion(columna, fila);Console.WriteLine("█");
			}
			
			columna = 117; //lado derecho
			for( fila = 1; fila<=28; fila=fila+1 ){
				Posicion(columna, fila);Console.WriteLine("█");
			}
		}
		
		static void error()
		{
			
			int fila, columna;
			
			Color("rojo", "blanco");
			fila = 2; //linea superior
			for( columna = 35; columna<=85; columna=columna+1 ){
				Posicion(columna, fila);Console.WriteLine("▄");
			}
			
			fila = 26; //linea inferior
			for( columna = 35; columna<=85; columna=columna+1 ){
				Posicion(columna, fila);Console.WriteLine("▀");
			}
			
			columna = 35; //lado iskierdo
			for( fila = 3; fila<=25; fila=fila+1 ){
				Posicion(columna, fila);Console.WriteLine("█");
			}
			
			columna = 85; //lado derecho
			for( fila = 3; fila<=25; fila=fila+1 ){
				Posicion(columna, fila);Console.WriteLine("█");
			}
			
		}
		


		
		static void Animacion(string texto)
		{
			foreach (char caracter in texto)
			{
				Console.Write(caracter);
				Thread.Sleep(1);
			}
		}
		
		static void Reset()
		{
			Console.Clear();
		}
		
		static void Reiniciar()
		{
			// Obtiene la ubicación del archivo ejecutable actual
			string filePath = Process.GetCurrentProcess().MainModule.FileName;

			// Inicia una nueva instancia del proceso actual
			Process.Start(filePath);

			// Termina el proceso actual
			Environment.Exit(0);
		}
		
		static void Posicion(int x, int y)
		{
			Console.SetCursorPosition(x, y);
		}
		
		static void Cursor(bool mostrar)
		{
			Console.CursorVisible = mostrar;
		}
		
		static void Tiempo(int milliseconds)
		{
			Thread.Sleep(milliseconds);
		}
		
		static void ColorRandomBlanco()
		{
			// Obtener todos los valores de ConsoleColor en un arreglo
			ConsoleColor[] colores = (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor));

			// Generar un objeto Random para elegir índices aleatorios
			Random rand = new Random();

			// Elegir aleatoriamente un color para el texto
			ConsoleColor colorTexto;
			do
			{
				colorTexto = colores[rand.Next(colores.Length)];
			} while (colorTexto == ConsoleColor.White || colorTexto == ConsoleColor.Gray || colorTexto == ConsoleColor.Yellow || colorTexto == ConsoleColor.Cyan || colorTexto == ConsoleColor.Black || colorTexto == ConsoleColor.DarkGray); // Evitar que el color del texto sea blanco

			// Establecer el color del texto y el fondo
			Console.ForegroundColor = colorTexto;
			Console.BackgroundColor = ConsoleColor.White; // Fondo blanco
		}
		
		static void ColorRandomNegro()
		{
			// Obtener todos los valores de ConsoleColor en un arreglo
			ConsoleColor[] colores = (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor));

			// Generar un objeto Random para elegir índices aleatorios
			Random rand = new Random();

			// Elegir aleatoriamente un color para el texto
			ConsoleColor colorTexto;
			do
			{
				colorTexto = colores[rand.Next(colores.Length)];
			} while (colorTexto == ConsoleColor.Black); // Evitar que el color del texto sea negro

			// Establecer el color del texto y el fondo
			Console.ForegroundColor = colorTexto;
			Console.BackgroundColor = ConsoleColor.Black; // Fondo negro
		}
		
		static void ColorCarga()
		{
			// Colores válidos para el texto
			ConsoleColor[] coloresTexto = { ConsoleColor.Blue, ConsoleColor.Black, ConsoleColor.Cyan, ConsoleColor.DarkBlue };

			// Generar un objeto Random para elegir índices aleatorios
			Random rand = new Random();

			// Elegir aleatoriamente un color para el texto
			ConsoleColor colorTexto = coloresTexto[rand.Next(coloresTexto.Length)];

			// Establecer el color del texto y el fondo
			Console.ForegroundColor = colorTexto;
			Console.BackgroundColor = ConsoleColor.White; // Fondo blanco
		}
		static void Azul(string fondo)
		{
			// Colores válidos para el texto
			ConsoleColor[] coloresTexto = { ConsoleColor.Blue, ConsoleColor.DarkBlue };

			// Generar un objeto Random para elegir índices aleatorios
			Random rand = new Random();

			// Elegir aleatoriamente un color para el texto
			ConsoleColor colorTexto = coloresTexto[rand.Next(coloresTexto.Length)];

			// Diccionario que mapea los nombres de los colores en español a enum ConsoleColor
			Dictionary<string, ConsoleColor> coloresFondo = new Dictionary<string, ConsoleColor>()
			{
				{"negro", ConsoleColor.Black},
				{"azul", ConsoleColor.Blue},
				{"cian", ConsoleColor.Cyan},
				{"gris", ConsoleColor.Gray},
				{"verde", ConsoleColor.Green},
				{"purpura", ConsoleColor.Magenta},
				{"rojo", ConsoleColor.Red},
				{"blanco", ConsoleColor.White},
				{"amarillo", ConsoleColor.Yellow}
			};

			// Convertir el nombre del color de fondo a enum ConsoleColor
			ConsoleColor colorFondo;
			if (coloresFondo.TryGetValue(fondo.ToLower(), out colorFondo))
			{
				// Establecer el color del texto y el fondo
				Console.ForegroundColor = colorTexto;
				Console.BackgroundColor = colorFondo;
			}
			else
			{
				Console.WriteLine("El color de fondo especificado no es válido.");
			}
		}
		
		static void Color(string colorTexto, string colorFondo)
		{
			Dictionary<string, ConsoleColor> coloresTexto = new Dictionary<string, ConsoleColor>
			{
				{"negro", ConsoleColor.Black},
				{"azul", ConsoleColor.Blue},
				{"cian", ConsoleColor.Cyan},
				{"azuloscuro", ConsoleColor.DarkBlue},
				{"cianoscuro", ConsoleColor.DarkCyan},
				{"grisoscuro", ConsoleColor.DarkGray},
				{"verdeoscuro", ConsoleColor.DarkGreen},
				{"moradooscuro", ConsoleColor.DarkMagenta},
				{"rojooscuro", ConsoleColor.DarkRed},
				{"amarillooscuro", ConsoleColor.DarkYellow},
				{"gris", ConsoleColor.Gray},
				{"verde", ConsoleColor.Green},
				{"morado", ConsoleColor.Magenta},
				{"rojo", ConsoleColor.Red},
				{"blanco", ConsoleColor.White},
				{"amarillo", ConsoleColor.Yellow}
			};

			Dictionary<string, ConsoleColor> coloresFondo = new Dictionary<string, ConsoleColor>
			{
				{"negro", ConsoleColor.Black},
				{"azul", ConsoleColor.Blue},
				{"cian", ConsoleColor.Cyan},
				{"azuloscuro", ConsoleColor.DarkBlue},
				{"cianoscuro", ConsoleColor.DarkCyan},
				{"grisoscuro", ConsoleColor.DarkGray},
				{"verdeoscuro", ConsoleColor.DarkGreen},
				{"moradooscuro", ConsoleColor.DarkMagenta},
				{"rojooscuro", ConsoleColor.DarkRed},
				{"amarillooscuro", ConsoleColor.DarkYellow},
				{"gris", ConsoleColor.Gray},
				{"verde", ConsoleColor.Green},
				{"morado", ConsoleColor.Magenta},
				{"rojo", ConsoleColor.Red},
				{"blanco", ConsoleColor.White},
				{"amarillo", ConsoleColor.Yellow}
			};

			if (coloresTexto.ContainsKey(colorTexto.ToLower()) && coloresFondo.ContainsKey(colorFondo.ToLower()))
			{
				Console.ForegroundColor = coloresTexto[colorTexto.ToLower()];
				Console.BackgroundColor = coloresFondo[colorFondo.ToLower()];
			}
			else
			{
				Console.WriteLine("Color no válido.");
			}
		}
		
		static void Guardar(string archivo, string correo, string celular, string contraseña, string usuario, string puntos)
		{
			using (StreamWriter writer = new StreamWriter(archivo, append: true))
			{
				writer.WriteLine(correo);
				writer.WriteLine(celular);
				writer.WriteLine(contraseña);
				writer.WriteLine(usuario);
				writer.WriteLine(puntos);
				writer.WriteLine(); // Agregar una línea en blanco como separador entre usuarios
			}
		}

		static void Cuentas()
		{
			string archivo = "Datos.txt";
			string[] datosUsuarios = File.ReadAllLines(archivo);

			int cuentasPorColumna = 21;
			int columnaActual = 0;
			int filaActual = 7;
			int numeros = 1;

			for (int i = 0; i < datosUsuarios.Length; i += 6)
			{
				string usuarioAlmacenado = datosUsuarios[i + 3];
				
				// Calcular la posición en la columna
				int columnaX = 40 + (columnaActual * 20);
				
				// Imprimir el nombre del usuario
				Color("negro","blanco");
				Posicion(columnaX, filaActual);
				Console.WriteLine(numeros + ". " + usuarioAlmacenado);

				filaActual++;
				numeros++;

				// Si la fila actual supera el límite de cuentas por columna, reiniciamos la fila y pasamos a la siguiente columna
				if (filaActual >= 6 + cuentasPorColumna)
				{
					filaActual = 7;
					columnaActual++;
				}
			}
		}
		
		static string[] Cargar(string archivo)
		{
			// Leer todas las líneas del archivo y retornarlas como un arreglo de strings
			return File.ReadAllLines(archivo);
		}
		
		static void BorrarCuenta(string archivo, string usuarioAEliminar)
		{
			string[] datosUsuarios = File.ReadAllLines(archivo);
			List<string> datosActualizados = new List<string>();

			bool cuentaEliminada = false;
			for (int i = 0; i < datosUsuarios.Length; i += 6)
			{
				string correo = datosUsuarios[i];
				string celular = datosUsuarios[i + 1];
				string contraseñaAlmacenada = datosUsuarios[i + 2];
				string usuarioAlmacenado = datosUsuarios[i + 3];
				string puntos = datosUsuarios[i + 4];

				if (usuarioAlmacenado != usuarioAEliminar)
				{
					datosActualizados.Add(correo);
					datosActualizados.Add(celular);
					datosActualizados.Add(contraseñaAlmacenada);
					datosActualizados.Add(usuarioAlmacenado);
					datosActualizados.Add(puntos);
					datosActualizados.Add(""); // Línea en blanco entre cuentas
				}
				else
				{
					cuentaEliminada = true;
				}
			}

			if (cuentaEliminada)
			{
				File.WriteAllLines(archivo, datosActualizados);
				
				opcion = 1;
				
				Color("negro", "blanco");
				for (fila = 2; fila <=27; fila++) {
					for (columna = 35; columna <= 116; columna++) {
						Posicion(columna, fila);
						Console.WriteLine(" ");
					}
				}
				
				if(idioma == "español")
				{
					Color("rojo", "blanco");
					Posicion(66,10);
					Console.WriteLine("CUENTA ELIMINADA");
					
					Color("blanco", "negro");
					Posicion(36,26);
					Console.WriteLine("                    Preciona cualquier tecla para continuar                     ");
				}
				if(idioma == "english")
				{
					Color("rojo", "blanco");
					Posicion(66,10);
					Console.WriteLine("ACCOUNT DELETED");
					
					
					Color("blanco", "negro");
					Posicion(36,26);
					Console.WriteLine("                          Press any key to continue                             ");
				}
				
				string[] arteASCII =
				{
					"░░░░░▄▄▄▄▄░▄░▄░▄░▄",
					"▄▄▄▄██▄████▀█▀█▀█▀██▄",
					"▀▄▀▄▀▄████▄█▄█▄█▄█████",
					"▒▀▀▀▀▀▀▀▀██▀▀▀▀██▀▒▄██",
					"▒▒▒▒▒▒▒▒▀▀▒▒▒▒▀▀▄▄██▀▒"
				};

				for (int i = 0; i < arteASCII.Length; i++)
				{
					Color("negro", "blanco");
					Posicion(63, 13 + i);
					Console.WriteLine(arteASCII[i]);
				}
			}
		}
		
		static bool IniciarSesion(string usuario, string contraseña)
		{
			string archivo = "Datos.txt";
			string[] datosUsuarios = File.ReadAllLines(archivo);

			for (int i = 0; i < datosUsuarios.Length; i += 6)
			{
				string correo = datosUsuarios[i];
				string celular = datosUsuarios[i + 1];
				string contraseñaAlmacenada = datosUsuarios[i + 2];
				string usuarioAlmacenado = datosUsuarios[i + 3];


				// Comparar usuario y contraseña
				if (usuario == usuarioAlmacenado && contraseña == contraseñaAlmacenada)
				{
					if(idioma == "español")
					{
						Posicion(47,21);
						Color("blanco", "verdeoscuro");
						Console.WriteLine("INICIO DE SESION EXITOSO");
					}
					if(idioma == "english")
					{
						Posicion(47,21);
						Color("blanco", "verdeoscuro");
						Console.WriteLine("    SUCCESSFUL LOGIN");
					}
					
					Tiempo(150);
					
					return true; // Inicio de sesión exitoso
				}
				else if (usuario == usuarioAlmacenado && contraseña != contraseñaAlmacenada)
				{
					if(idioma == "español")
					{
						Posicion(49,21);
						Color("blanco", "verdeoscuro");
						Console.WriteLine("CONTRASEÑA INCORRECTA");
					}
					if(idioma == "english")
					{
						Posicion(51,21);
						Color("blanco", "verdeoscuro");
						Console.WriteLine("INCORRECT PASSWORD");
					}
					
					Color("negro", "negro");
					Precionar();
				}
			}

			return false; // Usuario o contraseña incorrectos
		}
		
		static void BuscarUsuario(string archivo, string usuarioBuscar)
		{

			// Leer datos del archivo y buscar la cuenta del usuario
			bool cuentaEncontrada = false;
			using (StreamReader reader = new StreamReader(archivo))
			{
				string correo, celular, contraseña, usuarioBuscado, puntos;

				while ((correo = reader.ReadLine()) != null)
				{
					celular = reader.ReadLine();
					contraseña = reader.ReadLine();
					usuarioBuscado = reader.ReadLine();
					puntos = reader.ReadLine();

					if (usuarioBuscado == usuarioBuscar)
					{
						
						if(usuarioBuscar == usuario)
						{
							ColorRandomBlanco();
							Posicion(58,10);
							Animacion(usuarioBuscar);
							Color("grisoscuro", "blanco");
							
							if(idioma == "español")
							{
								Animacion(" (tu)");
							}
							if(idioma == "english")
							{
								Animacion(" (you)");
							}
						}
						else
						{
							ColorRandomBlanco();
							Posicion(58,10);
							Animacion(usuarioBuscar);
						}
						
						Color("negro", "blanco");
						Posicion(58,14);
						Console.WriteLine(correo);
						Posicion(58,16);
						Console.WriteLine(celular);
						
						if(idioma == "español")
						{
							Posicion(58,18);
							Console.WriteLine("Puntos: " + puntos);
						}
						if(idioma == "english")
						{
							Posicion(58,18);
							Console.WriteLine("Poins: " + puntos);
						}
						
						puntosObtenidos = int.Parse(puntos);
						
						if(idioma == "español")
						{
							Color("negro", "blanco");
							Posicion(58,20);
							Console.Write("Su rango es ");
						}
						if(idioma == "english")
						{
							Color("negro", "blanco");
							Posicion(58,20);
							Console.Write("Your rank is ");
						}

						
						if(puntosObtenidos >= 0 && puntosObtenidos < 100)
						{
							ColorRandomBlanco();
							Console.Write("BRONCE");
						}
						if(puntosObtenidos >= 100 && puntosObtenidos < 200)
						{
							ColorRandomBlanco();
							Console.Write("PLATA");
						}
						if(puntosObtenidos >= 200 && puntosObtenidos < 300)
						{
							ColorRandomBlanco();
							Console.Write("PLATINO");
						}
						if(puntosObtenidos >= 300 && puntosObtenidos < 400)
						{
							ColorRandomBlanco();
							Console.Write("ORO");
						}
						if(puntosObtenidos >= 400 && puntosObtenidos < 500)
						{
							ColorRandomBlanco();
							Console.Write("ZAFIRO");
						}
						if(puntosObtenidos >= 500 && puntosObtenidos < 600)
						{
							ColorRandomBlanco();
							Console.Write("RUBI");
						}
						if(puntosObtenidos >= 600 && puntosObtenidos < 700)
						{
							ColorRandomBlanco();
							Console.Write("ESMERALDA");
						}
						if(puntosObtenidos >= 700 && puntosObtenidos < 800)
						{
							ColorRandomBlanco();
							Console.Write("DIAMANTE");
						}
						
						puntos = puntosObtenidos.ToString();
						
						cuentaEncontrada = true;
						break; // Salir del bucle una vez que se ha encontrado la cuenta
					}

					// Leer línea en blanco entre cuentas
					reader.ReadLine();
				}
			}

			if (!cuentaEncontrada)
			{
				Console.WriteLine("No se encontró la cuenta.");
			}
		}
		
		static void ImprimirDatos(string archivo, string usuario)
		{

			// Leer datos del archivo y buscar la cuenta del usuario
			bool cuentaEncontrada = false;
			using (StreamReader reader = new StreamReader(archivo))
			{
				string correo, celular, contraseña, usuarioActual, puntos;

				while ((correo = reader.ReadLine()) != null)
				{
					celular = reader.ReadLine();
					contraseña = reader.ReadLine();
					usuarioActual = reader.ReadLine();
					puntos = reader.ReadLine();

					if (usuarioActual == usuario)
					{
						Color("negro", "blanco");
						Posicion(60,10);
						Console.WriteLine(correo);
						Posicion(60,14);
						Console.WriteLine(celular);
						Posicion(60,18);
						Console.WriteLine(puntos);
						
						puntosObtenidos = int.Parse(puntos);

						if(idioma == "español")
						{
							Color("negro", "blanco");
							Posicion(60,22);
							Console.Write("Tu rango es ");
						}
						if(idioma == "english")
						{
							Color("negro", "blanco");
							Posicion(60,22);
							Console.Write("You rank is ");
						}
						
						if(puntosObtenidos >= 0 && puntosObtenidos < 100)
						{
							ColorRandomBlanco();
							Console.Write("BRONCE");
						}
						if(puntosObtenidos >= 100 && puntosObtenidos < 200)
						{
							ColorRandomBlanco();
							Console.Write("PLATA");
						}
						if(puntosObtenidos >= 200 && puntosObtenidos < 300)
						{
							ColorRandomBlanco();
							Console.Write("PLATINO");
						}
						if(puntosObtenidos >= 300 && puntosObtenidos < 400)
						{
							ColorRandomBlanco();
							Console.Write("ORO");
						}
						if(puntosObtenidos >= 400 && puntosObtenidos < 500)
						{
							ColorRandomBlanco();
							Console.Write("ZAFIRO");
						}
						if(puntosObtenidos >= 500 && puntosObtenidos < 600)
						{
							ColorRandomBlanco();
							Console.Write("RUBI");
						}
						if(puntosObtenidos >= 600 && puntosObtenidos < 700)
						{
							ColorRandomBlanco();
							Console.Write("ESMERALDA");
						}
						if(puntosObtenidos >= 700 && puntosObtenidos < 800)
						{
							ColorRandomBlanco();
							Console.Write("DIAMANTE");
						}
						
						puntos = puntosObtenidos.ToString();
						
						cuentaEncontrada = true;
						break; // Salir del bucle una vez que se ha encontrado la cuenta
					}

					// Leer línea en blanco entre cuentas
					reader.ReadLine();
				}
			}
		}
		static void ActualizarPuntos(string archivo, string usuario, int puntosExtras)
		{
			// Leer datos del archivo y buscar la cuenta del usuario
			List<string> lineas = new List<string>();
			bool cuentaEncontrada = false;
			
			using (StreamReader reader = new StreamReader(archivo))
			{
				string correo, celular, contraseña, usuarioActual, puntos;

				while ((correo = reader.ReadLine()) != null)
				{
					celular = reader.ReadLine();
					contraseña = reader.ReadLine();
					usuarioActual = reader.ReadLine();
					puntos = reader.ReadLine();

					if (usuarioActual == usuario)
					{
						// Sumar los puntos extras al valor actual
						int puntosActuales = int.Parse(puntos);
						int nuevosPuntos = puntosActuales + puntosExtras;
						puntos = nuevosPuntos.ToString();
						
						// Agregar los datos actualizados al usuario
						lineas.Add(correo);
						lineas.Add(celular);
						lineas.Add(contraseña);
						lineas.Add(usuarioActual);
						lineas.Add(puntos);
						lineas.Add("");

						cuentaEncontrada = true;
					}
					else
					{
						// Agregar los datos sin modificar al usuario actual
						lineas.Add(correo);
						lineas.Add(celular);
						lineas.Add(contraseña);
						lineas.Add(usuarioActual);
						lineas.Add(puntos);
						lineas.Add("");
					}

					// Leer línea en blanco entre cuentas
					reader.ReadLine();
				}
			}

			// Escribir los datos actualizados en el archivo
			if (cuentaEncontrada)
			{
				using (StreamWriter writer = new StreamWriter(archivo))
				{
					foreach (string linea in lineas)
					{
						writer.WriteLine(linea);
					}
				}
			}
		}
		
	}
	
}//fin Program