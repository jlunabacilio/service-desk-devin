import { Tag } from 'antd';
import type { TicketStatus } from '@servicedesk/shared-types';

const STATUS_COLOR: Record<TicketStatus, string> = {
  'open': 'blue',
  'in-progress': 'orange',
  'resolved': 'green',
  'closed': 'default',
};

const STATUS_LABEL: Record<TicketStatus, string> = {
  'open': 'Open',
  'in-progress': 'In Progress',
  'resolved': 'Resolved',
  'closed': 'Closed',
};

interface TicketBadgeProps {
  status: TicketStatus;
}

export function TicketBadge({ status }: TicketBadgeProps) {
  return (
    <Tag color={STATUS_COLOR[status]}>{STATUS_LABEL[status]}</Tag>
  );
}
