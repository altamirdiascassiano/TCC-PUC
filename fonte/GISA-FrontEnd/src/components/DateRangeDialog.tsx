/* eslint-disable react/prop-types */
/* eslint-disable @typescript-eslint/no-var-requires */

import React from "react";
import {
	DateRangePicker,
	createStaticRanges
  } from "react-date-range";
  
import {
	addDays,
	endOfDay,
	startOfDay,
	startOfMonth,
	endOfMonth,
	addMonths,
	startOfWeek,
	endOfWeek,
	startOfYear,
	endOfYear,
	addYears
} from "date-fns";
  
import "react-date-range/dist/styles.css";
import "react-date-range/dist/theme/default.css";

import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';

const defineds = {
	startOfWeek: startOfWeek(new Date()),
	endOfWeek: endOfWeek(new Date()),
	startOfLastWeek: startOfWeek(addDays(new Date(), -7)),
	endOfLastWeek: endOfWeek(addDays(new Date(), -7)),
	startOfToday: startOfDay(new Date()),
	startOfLastSevenDay: startOfDay(addDays(new Date(), -7)),
	startOfLastThirtyDay: startOfDay(addDays(new Date(), -30)),
	startOfLastNintyDay: startOfDay(addDays(new Date(), -90)),
	endOfToday: endOfDay(new Date()),
	startOfYesterday: startOfDay(addDays(new Date(), -1)),
	endOfYesterday: endOfDay(addDays(new Date(), -1)),
	startOfMonth: startOfMonth(new Date()),
	endOfMonth: endOfMonth(new Date()),
	startOfLastMonth: startOfMonth(addMonths(new Date(), -1)),
	endOfLastMonth: endOfMonth(addMonths(new Date(), -1)),
	startOfYear: startOfYear(new Date()),
	endOfYear: endOfYear(new Date()),
	startOflastYear: startOfYear(addYears(new Date(), -1)),
	endOflastYear: endOfYear(addYears(new Date(), -1))
};

/*const initialState = {
  selection: {
    startDate: new Date(),
    endDate: addDays(new Date(), 30),
    key: "selection"
  },
  compare: {
    startDate: new Date(),
    endDate: addDays(new Date(), 30),
    key: "compare"
  }
};*/
  
const sideBarOptions: any = () => {
  const customDateObjects = [
    {
    label: "Hoje",
    range: () => ({
      startDate: defineds.startOfToday,
      endDate: defineds.endOfToday
    })
    },
    {
    label: "Amanhã",
    range: () => ({
      startDate: addDays(Date.now(), 1),
      endDate: addDays(Date.now(), 1)
    })
    },
    {
    label: "Próximos 7 Dias",
    range: () => ({
      startDate: addDays(Date.now(), 1),
      endDate: addDays(Date.now(), 7)
    })
    },
    {
    label: "Esta Semana",
    range: () => ({
      startDate: defineds.startOfWeek,
      endDate: defineds.endOfWeek
    })
    },
    {
    label: "Este Mês",
    range: () => ({
      startDate: defineds.startOfMonth,
      endDate: defineds.endOfMonth
    })
    },	  
    {
    label: "Última Semana",
    range: () => ({
      startDate: defineds.startOfLastWeek,
      endDate: defineds.endOfLastWeek
    })
    },
    {
    label: "Últimos 7 Dias",
    range: () => ({
      startDate: defineds.startOfLastSevenDay,
      endDate: defineds.endOfToday
    })
    },
    {
    label: "Últimos 30 Dias",
    range: () => ({
      startDate: defineds.startOfLastThirtyDay,
      endDate: defineds.endOfToday
    })
    }  
  ];  
  return customDateObjects;
};

const locales = require('react-date-range/dist/locale');

const RangeCalender = ({handleSelect, range}:any) => {

	const sideBar = sideBarOptions();
  
	const staticRanges = [
	  //...defaultStaticRanges,
	  ...createStaticRanges(sideBar)
	];

	return (
	  <div className="App">
      <div>
        <DateRangePicker
          ranges={[range.selection]}
          onChange={handleSelect}
          months={1}
          minDate={addDays(new Date(), -90)}
          maxDate={addDays(new Date(), 365)}
          direction="horizontal"
          locale={locales.ptBR}
          dateDisplayFormat="dd/MM/yyyy"
          showMonthAndYearPickers
          staticRanges={staticRanges}
          inputRanges={[]}		
        />
      </div>
	  </div>
	);
}

export interface Props {
  open: boolean;  
  range: any
  handleApply: (value: any) => void;
  handleSelect: (value: any) => void; 
  handleCancel: (value: any) => void;
}

const DateRangeDialog = (props: Props) => {

  const { handleApply, open, handleSelect, range, handleCancel} = props;

  return (
    <div>      
      <Dialog open={open} onClose={handleCancel} aria-labelledby="max-width-dialog-title" maxWidth={false}>
        <div style={{width: '100%'}}>
          <DialogTitle id="form-dialog-title">Selecione o Período</DialogTitle>
          <DialogContent>
            <RangeCalender 
              handleSelect={handleSelect}
              range={range}
            />
          </DialogContent>          
        </div>
        <DialogActions>
            <Button onClick={handleCancel} color="primary">
              Cancelar
            </Button>
            <Button onClick={handleApply} color="primary">
              Aplicar
            </Button>
          </DialogActions>
      </Dialog>
    </div>
  );
}

export default DateRangeDialog;
