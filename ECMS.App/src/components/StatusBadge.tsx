import React from 'react';

const statusConfig = {
  0: { label: 'Draft', color: 'gray' },
  1: { label: 'Queued', color: 'orange' },
  2: { label: 'Processing', color: 'blue' },
  3: { label: 'Completed', color: 'green' }
};

const StatusBadge = ({ status }) => {
  const config = statusConfig[status];
  return (
    <span className={`badge badge-${config.color}`}>
      {config.label}
    </span>
  );
};

export default StatusBadge;
