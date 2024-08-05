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
  mainAmount: number | null = null;
  subAmount: number | null = null;

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

  isValidAmount(): boolean {
    const totalAmount = (this.mainAmount ?? 0) + (this.subAmount ? this.subAmount / 100 : 0);
    return totalAmount >= 0.5;
  }

  sendMoney() {
    if (!this.selectedRecipient || !this.recipients.includes(this.selectedRecipient)) {
      alert('El destinatario es inválido.');
      return;
    }

    if (!this.isValidAmount()) {
      alert('La cantidad mínima a enviar es 0.5.');
      return;
    }

    const mainAmount = this.mainAmount ?? 0;
    const subAmount = this.subAmount ?? 0;
    const totalAmount = mainAmount + subAmount / 100;

    alert(`Enviando ${totalAmount.toFixed(2)} al destinatario ${this.selectedRecipient}.`);
  }
}