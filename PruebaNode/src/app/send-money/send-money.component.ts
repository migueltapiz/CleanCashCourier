import { Component } from '@angular/core';

@Component({
  selector: 'pm-send-money',
  templateUrl: './send-money.component.html',
  styleUrl: './send-money.component.css'
})
export class SendMoneyComponent {
  recipientFilter = '';
  recipients = [
    'John Doe', 'Jane Smith', 'Alice Johnson', 'Bob Brown', 'Charlie Davis', 'David Wilson',
    'Eve White', 'Frank Green', 'Grace Adams', 'Hank Thompson', 'Ivy Clark', 'Jack Lewis',
    'Karen Walker', 'Liam Harris', 'Mia Robinson', 'Noah Young', 'Olivia Martinez', 'Paul Hall',
    'Quincy Allen', 'Rachel Wright', 'Sam King', 'Tina Scott', 'Uma Hill', 'Victor Moore',
    'Wendy Turner', 'Xander Phillips', 'Yara Cooper', 'Zane Rogers', 'Zoey Stewart', 'Oscar Perry'
  ];
  filteredRecipients: string[] = [];
  selectedRecipient: string | null = null;
  amount: string = '';
  currency: string = 'EUR';

  filterRecipients() {
    this.filteredRecipients = this.recipients.filter(recipient =>
      recipient.toLowerCase().includes(this.recipientFilter.toLowerCase())
    );
  }

  selectRecipient(recipient: string) {
    this.selectedRecipient = recipient;
    this.recipientFilter = recipient;
    this.filteredRecipients = [];
  }

  validateAmount(event: Event) {
    const input = event.target as HTMLInputElement;
    let value = input.value;

    // Replace commas with periods and remove invalid characters
    value = value.replace(/,/g, '.').replace(/[^0-9.]/g, '');

    // Prevent multiple periods
    const parts = value.split('.');
    if (parts.length > 2) {
      value = parts[0] + '.' + parts.slice(1).join('');
    }

    // Ensure at least one digit before the period
    if (value.startsWith('.')) {
      value = '0' + value;
    }

    // Update the input value
    this.amount = value;

    // Update the input element value to ensure correct formatting
    input.value = value;
  }

  isValidAmount(): boolean {
    const numericAmount = parseFloat(this.amount.replace(',', '.'));
    return numericAmount >= 0.5;
  }

  sendMoney() {
    if (!this.selectedRecipient || !this.recipients.includes(this.selectedRecipient)) {
      alert('El destinatario es inválido.');
      return;
    }

    if (!this.isValidAmount()) {
      alert('La cantidad mínima a enviar es 0,5.');
      return;
    }

    const numericAmount = parseFloat(this.amount.replace(',', '.'));
    alert(`Enviando ${numericAmount.toFixed(2)} ${this.currency} al destinatario ${this.selectedRecipient}.`);
  }
}