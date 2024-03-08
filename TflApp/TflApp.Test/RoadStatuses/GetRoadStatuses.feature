Feature: Get Road Statuses
	A short summary of the feature

Scenario: Get road status by id - Happy Path
	Given A valid road id "A2"
	When The client runs
	Then The road display name is "A2"

Scenario: Get road status severity by id and date range
	Given A valid road id and date range
		| Road id | Start date | End date   |
		| A2      | 2024-02-01 | 2024-02-02 |
	When The client runs
	Then The road status severity is displayed as "Road Status"

Scenario: Get road severity description by id and date range
	Given A valid road id and date range
		| Road id | Start date | End date   |
		| A2      | 2024-02-01 | 2024-02-02 |
	When The client runs
	Then The road status severity description is displayed as "Road Status Description"

Scenario: Get status failure - Informative error
	Given An invalid road id "XY"
	When The client runs
	Then The response is returned with informative error message "The following road id is not recognised: XY"

Scenario: Get status failure - Non-zero error code
	Given An invalid road id "XY"
	When The client runs
	Then The application exits with system error code

Scenario: Get status failure - Null id
	Given An invalid road id ""
	When The client runs
	Then The response is returned with error message "Request road id cannot be null or empty."