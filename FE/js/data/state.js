export const state = {
    userId: Number(localStorage.getItem('userId')),
    currentMonth: Number(sessionStorage.getItem('month')),
    currentYear: Number(sessionStorage.getItem('year')),
};

export function setMonthYear() {
    const date = new Date();
    state.currentMonth = date.getMonth();
    state.currentYear = date.getFullYear();
    sessionStorage.setItem('month', month);
    sessionStorage.setItem('year', year);
}

export function updateMonth(month) {
    state.currentMonth = month;
    sessionStorage.setItem('month', month);
}

export function updateYear(year) {
    state.currentYear = year;
    sessionStorage.setItem('year', year);
}
