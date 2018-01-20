using System.Collections;

public static class Codigos {
	private static string primeroMaterial = "E64A4AFF";
	private static string segundoMaterial = "659DACFF";
    private static string successMaterial = "00C853";
    private static string nullMaterial = "E64A4AFF";
    private static string punteroMaterial = "FFA726";

    private static string primeroMaterialTagOpen = "<color=#"+ primeroMaterial +">";
    private static string segundoMaterialTagOpen = "<color=#" + segundoMaterial + ">";
    private static string successMaterialTagOpen = "<color=#" + successMaterial + ">";
    private static string nullMaterialTagOpen = "<color=#" + nullMaterial + ">";
    private static string punteroMaterialTagOpen = "<color=#" + punteroMaterial + ">";
    private static string colorTagClose = "</color>";

    public static int CHECK_ES_MENOR = 1;
    public static int END_INSERTAR = 2;

    public static string estructura =
        "struct nodo{" + "\n" +
        "\t" + "int " + segundoMaterialTagOpen + "dato" + colorTagClose + ";" + "\n" +
        "\t" + "struct nodo*sig;" + "\n" +
        "\r" + "}*cab,*actual,*anterior,*nuevo,*inicio,*fin,*sig,*aux, *primero;";

    public static string declaracion =
            "Nodo primero;" + "\n" +
            "public pila(){" + "\n" +
            "\t" + primeroMaterialTagOpen + "primero=NULL;" + colorTagClose + "\n" +
            "\r" + "}";
    public static string insertarInicio = 
            "public void insertar(int x){" + "\n" + 
            "\t" + "Nodo" + segundoMaterialTagOpen + " nuevo" + colorTagClose + " = new Nodo();" + "\n" +
            "\t" + segundoMaterialTagOpen + "nuevo" + colorTagClose + "->dato = x;" + "\n" +
            "\t" + segundoMaterialTagOpen + "nuevo" + colorTagClose + "->sig = " + nullMaterialTagOpen + "NULL;" + colorTagClose + "\n" +
            "\r" +
            "}";

	public static string codigoBuscarNodo = "Nodo actual = <b><color=#"+ primeroMaterial +">primero</color></b>;\n\tNodo anterior = null;\n\twhile(actual!=null){\n\t\tif (actual->dato==x){\n\t\t\tbreak;\n\t\t}\n\t\tanterior=actual;\n\t\tactual=actual->sig;\n\t}";
    public static string codigoEliminar = "Destroy(actual)";

    public static string getFailString(string text)
    {
        return primeroMaterialTagOpen + text + colorTagClose;
    }

    public static string getSuccessString(string text)
    {
        return successMaterialTagOpen + text + colorTagClose;
    }

    public static class ListaLeccion
    {
        public static string introduccion = "Las listas son estructuras de datos usadas que estan ordenadas de acuerdo a ciertos criterios, donde cada nodo tiene asignado una direccion de memoria y todo nodo tiene como minimo dos elementos y un puntero a la siguiente direccion de memoria. Asi mismo, el ultimo nodo apunta a una direccion de vacio";
        public static string insertar =
            "public void insertar(int x){" + "\n" +
            "\t" + "if(primero==NULL){" + "\n" +
                "\t\t" + "Nodo" + segundoMaterialTagOpen + " nuevo" + colorTagClose + " = new Nodo();" + "\n" +
                "\t\t" + segundoMaterialTagOpen + "nuevo" + colorTagClose + "->dato = x;" + "\n" +
                "\t\t" + segundoMaterialTagOpen + "nuevo" + colorTagClose + "->sig = " + nullMaterialTagOpen + "NULL;" + colorTagClose + "\n" +
                "\t\t" + primeroMaterialTagOpen + "primero" + colorTagClose + " = " + segundoMaterialTagOpen + "nuevo;" + colorTagClose + "\n" +
                "\r" +
            "}" +
            "\t" + "if((inicio!=NULL)&&(num<inicio->dato))" + "\n" +
            "\t\t" + "nuevo";
        public static string insertarOrdenadoCompleto = 
                "if((inicio!=NULL)&&(num<inicio->dato))\n" +
                "{\n" +
                "    nuevo= new nodo;\n" +
                "    nuevo->dato=num;\n" +
                "    nuevo->sig=inicio;\n" +
                "    inicio=nuevo;\n" +
                "}\n" +
                "else\n" +
                "{\n" +
                "    actual=inicio;\n" +
                "    while(actual!=NULL)\n" +
                "    {\n" +
                "        if(num < actual->dato) break;\n" +
                "        anterior=actual;\n" +
                "        actual=actual->sig;\n" +
                "    }\n" +
                "    nuevo=new nodo;\n" +
                "    nuevo->dato=num;\n" +
                "    nuevo->sig=actual;\n" +
                "    anterior->sig=nuevo;\n" +
                "}\n" +
                "return;";
        public static string insertarOrdenadoPaso =
                "if( num > inicio->dato )\n" +
                "{\n" +
                "    actual=inicio;\n" +
                "    while(actual!=NULL)\n" +
                "    {\n" +
                "        if(num < actual->dato) break;\n" +
                "        anterior=actual;\n" +
                "        actual=actual->sig;\n" +
                "    }\n" +
                "    nuevo=new nodo;\n" +
                "    nuevo->dato=num;\n" +
                "    nuevo->sig=actual;\n" +
                "    anterior->sig=nuevo;\n" +
                "}";
        public static string insertarOrdenadoPasoEncontrado =
                "if( num > inicio->dato )\n" +
                "{\n" +
                "    actual=inicio;\n" +
                "    while(actual!=NULL)\n" +
                "    {\n" +
                "        " + segundoMaterialTagOpen + "if(num < actual->dato) break;" + colorTagClose + "\n" +
                "        anterior=actual;\n" +
                "        actual=actual->sig;\n" +
                "    }\n" +
                "    nuevo=new nodo;\n" +
                "    nuevo->dato=num;\n" +
                "    nuevo->sig=actual;\n" +
                "    anterior->sig=nuevo;\n" +
                "}";
        public static string getInsertarOrdenadoPaso(bool found)
        {
            string ifMaterial = found ? successMaterialTagOpen : nullMaterialTagOpen;
            string actualMaterial = found ? nullMaterialTagOpen : successMaterialTagOpen;
            return
                punteroMaterialTagOpen + "actual" + colorTagClose + " = inicio;\n" +
                "while(" + punteroMaterialTagOpen + "actual" + colorTagClose + "!=NULL)\n" +
                "{\n" +
                "    " + ifMaterial + "if(num < actual->dato) break;" + colorTagClose + "\n" +
                "    " + actualMaterial + "anterior=" + punteroMaterialTagOpen + "actual" + colorTagClose + ";\n" +
                "    " + punteroMaterialTagOpen + "actual" + colorTagClose + "=actual->sig;\n" + colorTagClose + "\n" +
                "}";
        }

        public static string insertarOrdenadoIfInicio =
                "if(num < inicio->dato )\n" +
                "{\n" +
                "    nuevo= new nodo;\n" +
                "    nuevo->dato=num;\n" +
                "    nuevo->sig=inicio;\n" +
                "    inicio=nuevo;\n" +
                "}";
        public static string insertarOrdenadoFinal =
                "nuevo=new nodo;\n" +
                "nuevo->dato=num;\n" +
                "nuevo->sig=actual;\n" +
                "anterior->sig=nuevo;\n";

        public static string insertarOrdenadoFinal2(string num, string sig)
        {
            return
                "nuevo->dato = " + num + "\n" +
                "nuevo->sig = " + sig + "\n";
        } 

        public static string insertarOrdenadoInicio =
                "if((" + primeroMaterialTagOpen + "primero" + colorTagClose + "!=" + nullMaterialTagOpen + "NULL;" + colorTagClose + ")&&(x<" + primeroMaterialTagOpen + "primero" + colorTagClose + "->dato))\n" +
                "{\n" +
                "    " + segundoMaterialTagOpen + "nuevo" + colorTagClose + "= new nodo;\n" +
                "    " + segundoMaterialTagOpen + "nuevo" + colorTagClose + "->dato=x;\n" +
                "    " + segundoMaterialTagOpen + "nuevo" + colorTagClose + "->sig=" + primeroMaterialTagOpen + "primero" + colorTagClose + ";\n" +
                "    " + primeroMaterialTagOpen + "primero" + colorTagClose + "=" + segundoMaterialTagOpen + "nuevo" + colorTagClose + ";\n" +
                "}";
        public static string insertarOrdenadoBusqueda =
                "if((" + primeroMaterialTagOpen + "primero" + colorTagClose + "!=" + nullMaterialTagOpen + "NULL;" + colorTagClose + ")&&(x>" + primeroMaterialTagOpen + "primero" + colorTagClose + "->dato))\n" +
                "{\n" +
                "    actual=" + primeroMaterialTagOpen + "primero" + colorTagClose + ";\n" +
                "    while(actual!=" + nullMaterialTagOpen + "NULL;" + colorTagClose + ")\n" +
                "    {\n" +
                "        if(x < actual->dato) break;\n" +
                "        anterior=actual;\n" +
                "        actual=actual->sig;\n" +
                "    }\n" +
                "    " + segundoMaterialTagOpen + "nuevo" + colorTagClose + "=new nodo;\n" +
                "    " + segundoMaterialTagOpen + "nuevo" + colorTagClose + "->dato=x;\n" +
                "    " + segundoMaterialTagOpen + "nuevo" + colorTagClose + "->sig=actual;\n" +
                "    anterior->sig=" + segundoMaterialTagOpen + "nuevo" + colorTagClose + ";\n" +
                "}";

        //Codigos para Eliminar
        public static string EliminarInicio =
                "if((" + primeroMaterialTagOpen + "primero" + colorTagClose + "->dato" + nullMaterialTagOpen + "NULL;" + colorTagClose + ")&&(x<" + primeroMaterialTagOpen + "primero" + colorTagClose + "->dato))\n" +
                "{\n" +
                "    " + segundoMaterialTagOpen + "nuevo" + colorTagClose + "= new nodo;\n" +
                "    " + segundoMaterialTagOpen + "nuevo" + colorTagClose + "->dato=x;\n" +
                "    " + segundoMaterialTagOpen + "nuevo" + colorTagClose + "->sig=" + primeroMaterialTagOpen + "primero" + colorTagClose + ";\n" +
                "    " + primeroMaterialTagOpen + "primero" + colorTagClose + "=" + segundoMaterialTagOpen + "nuevo" + colorTagClose + ";\n" +
                "}";

        public static string getEliminarPaso(bool found)
        {
            string ifMaterial = found ? successMaterialTagOpen : nullMaterialTagOpen;
            string actualMaterial = found ? nullMaterialTagOpen : successMaterialTagOpen;
            return
                punteroMaterialTagOpen + "actual" + colorTagClose + " = inicio;\n" +
                "while(" + punteroMaterialTagOpen + "actual" + colorTagClose + "!=NULL)\n" +
                "{\n" +
                "    " + ifMaterial + "if(num == actual->dato) break;" + colorTagClose + "\n" +
                "    " + actualMaterial + "anterior=" + punteroMaterialTagOpen + "actual" + colorTagClose + ";\n" +
                "    " + punteroMaterialTagOpen + "actual" + colorTagClose + "=actual->sig;\n" + colorTagClose + "\n" +
                "}";
        }

        public static string EliminarFinal =
               "anterior->sig=actual->sig;\n" +
               "delete(actual);\n";

    }


    public static class PilasLeccion
    {
        
        public static string insertar =
            "public void insertar(int x){" + "\n" +
            "\t" + "Nodo" + segundoMaterialTagOpen + " nuevo" + colorTagClose + " = new Nodo();" + "\n" +
            "\t" + segundoMaterialTagOpen + "nuevo" + colorTagClose + "->dato = x;" + "\n" +
            "\t" + segundoMaterialTagOpen + "nuevo" + colorTagClose + "->sig = " + primeroMaterialTagOpen + "primero;" + colorTagClose + "\n" +
            "\t" + primeroMaterialTagOpen + "primero" + colorTagClose + " = " + segundoMaterialTagOpen + "nuevo;" + colorTagClose + "\n" + 
            "\r" + 
            "}";

        public static string eliminar =
            ";";
    }

    public static class ColasLeccion
    {
        public static string insertar =
            "public void insertar(int x){" + "\n" +
            "\t" + "Nodo" + segundoMaterialTagOpen + " nuevo" + colorTagClose + " = new Nodo();" + "\n" +
            "\t" + segundoMaterialTagOpen + "nuevo" + colorTagClose + "->dato = x;" + "\n" +
            "\t" + segundoMaterialTagOpen + "nuevo" + colorTagClose + "->sig = " + primeroMaterialTagOpen + "primero;" + colorTagClose + "\n" +
            "\t" + segundoMaterialTagOpen + "auxiliar" + colorTagClose + "->sig = " + primeroMaterialTagOpen + "nuevo;" + colorTagClose + "\n" +
            "\t" + primeroMaterialTagOpen + "auxiliar" + colorTagClose + " = " + segundoMaterialTagOpen + "nuevo;" + colorTagClose + "\n" + 
            "\r" + 
            "}";
    }

    public interface ICodigo
    {
        void onWriteCode(int listIndex, string codigo);
    }
}
