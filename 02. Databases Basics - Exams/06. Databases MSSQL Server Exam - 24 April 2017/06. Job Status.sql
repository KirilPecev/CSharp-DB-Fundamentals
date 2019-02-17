SELECT Status, IssueDate
FROM Jobs
WHERE [Status] NOT IN('Finished')
order by IssueDate, JobId