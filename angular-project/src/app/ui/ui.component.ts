import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FlatTreeControl } from '@angular/cdk/tree';
import {
  MatTreeFlatDataSource,
  MatTreeFlattener,
} from '@angular/material/tree';
import { BehaviorSubject } from 'rxjs';

import data from '../../../db/userdata.json'; // lee el archivo que conserva el usuario y la base de datos
import * as datatable from '../../../db/tabledata.json'; // lee el archivo que conserva la informacion solicitada por el SELECT

interface TreeNode {
  name: string;
  children?: TreeNode[];
}

interface FlatNode {
  expandable: boolean;
  name: string;
  level: number;
}

interface Node {
  name: string;
  children?: Node[];
}

function getLevel(node: FlatNode): number {
  return node.level;
}

function isExpandable(node: FlatNode): boolean {
  return node.expandable;
}

function getChildren(node: TreeNode): TreeNode[] | undefined {
  return node.children;
}

@Component({
  selector: 'app-ui',
  templateUrl: './ui.component.html',
  styleUrls: ['./ui.component.css'],
})
export class UiComponent {

  public jsonData: any = (datatable as any).default; // guarda la información leída del json
  public columnas: string[] = []; // variable para las colmnas de la tabla

  botonVisible: boolean = false; // inicio del botón del commit

  // habilta el botón del commit cuando hay un cambio en los archivos
  mostrarBoton() {
    this.botonVisible = !this.botonVisible;
  }

  // Lógica de comandos SQL
  instruccion_split: string[] = []; // conserva el comando en split
  instruccion: string = ''; // comando SQL

  // divide el texto ingresado por el usuario de acuerdo al comando SQL
  dividirString() {
    this.instruccion_split = this.instruccion.split(' ');
    this.analyze_command();
  }

  // limpia el textarea del SQL
  limpiarTextarea() {
    this.instruccion = '';
  }

  // Analiza los comandos ingresados por el usuario
  analyze_command() {
    switch (this.instruccion_split[0]) {
      case 'CREATE':
        switch (this.instruccion_split[1]) {
          case 'TABLE':
            console.log('COMMAND: CREATE TABLE');

            const table_name = /TABLE\s+(.*?)\s+\(/i;
            const match_tn = this.instruccion.match(table_name);

            if (match_tn && match_tn[1]) {
              const attributes = match_tn[1]
                .split(',')
                .map((attribute) => attribute.trim());
              console.log(attributes);
            }

            const attributes_table = /\((.*?)\)/i;
            const match_at = this.instruccion.match(attributes_table);

            if (match_at && match_at[1]) {
              const attributes = match_at[1]
                .split(',')
                .map((attribute) => attribute.trim());
              console.log(attributes);
            }
            break;

          default:
            console.log('ERROR: COMMAND DOES NOT EXIST!');
            break;
        }
        break;
      case 'INSERT':
        switch (this.instruccion_split[1]) {
          case 'INTO':
            console.log('COMMAND: INSERT INTO');

            const values = /VALUES\s+([\s\S]+)/i;
            const match = this.instruccion.match(values);

            if (match && match[1]) {
              const valuesString = match[1];
              const values = valuesString
                .split('),')
                .map((value) => value.replace(/\(|\)/g, '').trim());

              const objectsCount = values.length;
              console.log('Total values:', objectsCount);

              for (const value of values) {
                const attributes = value
                  .split(',')
                  .map((attribute) => attribute.trim());
                console.log('Values:', attributes);
              }
            }
            break;

          default:
            console.log('ERROR: COMMAND DOES NOT EXIST!');
            break;
        }
        break;
      case 'SELECT':
        const hasInnerJoin = this.instruccion.toUpperCase().includes('INNER JOIN');

        const conditionsRegexe = /WHERE\s+(.+)/i;
        const match = this.instruccion.match(conditionsRegexe);

        if (match && match[1]) {
          const conditionsString = match[1].replace(/AND|OR/gi, '');
          const conditions = conditionsString.split(/\s+/).filter(condition => condition !== '');

          console.log('Conditions:');
          for (const condition of conditions) {
            console.log(condition);
          }
        }

        // Si tiene INNER JOIN
        if (hasInnerJoin) {
          const selectRegex = /^SELECT\s+(.*?)\s+FROM\s+(\w+)\s+INNER JOIN\s+(\w+)\s+ON\s+([\s\S]+?)(?:\s+WHERE|$)/i;
          const match = this.instruccion.match(selectRegex);

          if (match) {
            const attributes = match[1].split(',').map(attribute => attribute.trim());
            const tableName = match[2];
            const joinTableName = match[3];
            const joinCondition = match[4];

            console.log('Attributes:', attributes);
            console.log('Table name:', tableName);
            console.log('Inner Join table:', joinTableName);
            console.log('Conditions:', joinCondition);
          }

        // Si no tiene INNER JOIN en la secuencia
        } else {
          const selectRegex = /^SELECT\s+(.*?)\s+FROM\s+(\w+)\s*(?:\s+WHERE|$)/i;
          const match = this.instruccion.match(selectRegex);

          if (match) {
            const attributes = match[1].split(',').map(attribute => attribute.trim());
            const tableName = match[2];

            console.log('Attributes:', attributes);
            console.log('Table name:', tableName);
          }
        }

        break;
      case 'UPDATE':
        const tableName = /UPDATE\s+(\w+)/i;
        const attributesValues = /SET\s+([\s\S]+?)\s+WHERE/i;
        const conditionsRegexx = /WHERE\s+([\s\S]+)/i;

        const tableNameMatch = this.instruccion.match(tableName);
        const attributesValuesMatch = this.instruccion.match(
          attributesValues
        );
        const conditionsMatchx = this.instruccion.match(conditionsRegexx);

        if (tableNameMatch && tableNameMatch[1]) {
          const tableName = tableNameMatch[1];
          console.log('Table name:', tableName);
        }

        if (attributesValuesMatch && attributesValuesMatch[1]) {
          const attributesValuesString = attributesValuesMatch[1];
          const attributesValues = attributesValuesString
            .split(',')
            .map((attributeValue) => attributeValue.trim());

          for (const attributeValue of attributesValues) {
            const [attribute, value] = attributeValue
              .split('=')
              .map((part) => part.trim());
            console.log('Atributo:', attribute);
            console.log('Nuevo valor:', value);
          }
        }

        if (conditionsMatchx && conditionsMatchx[1]) {
          const conditionsString = conditionsMatchx[1];
          const conditions = conditionsString.split(/\s+(AND|OR)\s+/i);

          for (const condition of conditions) {
            console.log('Condición:', condition);
          }
        }

        break;
      case 'DELETE':
        const deleteRegex = /^DELETE\s+FROM\s+(\w+)/i;
        const conditionsRegex = /WHERE\s+([\s\S]+)/i;

        const deleteMatch = this.instruccion.match(deleteRegex);
        const conditionsMatch = this.instruccion.match(conditionsRegex);

        if (deleteMatch && deleteMatch[1]) {
          const tableName = deleteMatch[1];
          console.log('Table name:', tableName);

          if (conditionsMatch && conditionsMatch[1]) {
            const conditionsString = conditionsMatch[1];
            const conditions = conditionsString.split(/\s+(AND|OR)\s+/i);

            for (const condition of conditions) {
              console.log('Condition:', condition);
            }
          } else {
            console.log('No hay datos (Condiciones)');
          }
        } else {
          console.log('ERROR: COMMAND DOES NOT EXIST!');
        }
        break;
      default:
        console.log('ERROR: COMMAND DOES NOT EXIST!');
        break;
    }
  }
  public showTable: boolean = false;

  public toggleTable(): void {
    this.showTable = !this.showTable;
  }

  ventanaVisible: boolean = false; // condicion para mostrar la ventana al presionar el boton About

  // muestra la ventana con los comandos
  mostrar_info() {
    this.ventanaVisible = true;
  }

  // cierra la ventana
  cerrarVentana() {
    this.ventanaVisible = false;
  }

  // permite la expansion de nodos, esto para el Xml Store Tree
  private _transformer = (node: Node, level: number) => {
    return {
      expandable: !!node.children && node.children.length > 0,
      name: node.name,
      level: level,
    };
  };

  // Lógica para mostrar la estructura del arbol
  treeControl = new FlatTreeControl<FlatNode>(getLevel, isExpandable);
  treeFlattener = new MatTreeFlattener(
    this._transformer,
    (node) => node.level,
    (node) => node.expandable,
    (node) => node.children
  );
  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  constructor() {
    this.dataSource.data = data; // toma la informacion del json
  }

  ngOnInit() {

    // si hay uno o más datos en el json, toma las columnas para crear las tablas
    if (this.jsonData.length > 0) {
      this.columnas = Object.keys(this.jsonData[0]);
    }
  }

  hasChild = (_: number, node: FlatNode) => node.expandable;
}
