#!/bin/bash
echo $(dotnet ef --startup-project ../Web.Api migrations add $1)
echo $(dotnet ef --startup-project ../Web.Api database update)